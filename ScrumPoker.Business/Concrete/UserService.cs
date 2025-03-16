using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ScrumPoker.Business.Abstract;
using ScrumPoker.Common.Consts;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.Dto;
using ScrumPoker.Helper;
using System.Text.Json;
using ScrumPoker.Business.Abstract;

namespace ScrumPoker.Business.Concrete;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly ILoggerService _nLogService;
    private readonly IUserDal _userDal;
    private readonly IAuthService _authService;
    private readonly IUserTokenService _userTokenService;
    private readonly IEmailService _emailService;
    private AuthSettingsDto _authSettings;


    public UserService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILoggerService nLogService, IUserDal userDal, IAuthService authService, IUserTokenService userTokenService, IEmailService emailService, IOptions<AuthSettingsDto> authSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _nLogService = nLogService;
        _userDal = userDal;
        _authService = authService;
        _userTokenService = userTokenService;
        _emailService = emailService;
        _authSettings = authSettings.Value;
    }

    public async Task<Guid> Create(CreateUserDto request)
    {
        return await _userDal.Create(request);
    }

    public async Task<bool> Update(UpdateUserDto request)
    {
        return await _userDal.Update(request);
    }

    public async Task<UserDto> Get(UserGetRequestDto request)
    {
        return await _userDal.Get(request);
    }

    public async Task<bool> UpdatePassword(AuthResetPasswordRequestDto request)
    {
        return await _userDal.UpdatePassword(request);
    }

    public async Task<UserDataDto> LoginOperation(LoginOperationDto request)
    {
        var user = await _userDal.Get(new UserGetRequestDto() { Email = request.Email });
        if (user != null)
        {
            bool isValidPassword = await PasswordHelper.VerifyPassword(new VerifyPasswordRequestDto()
            {
                Password = request.Password,
                StoredHash = Convert.FromBase64String(user.PasswordHash),
                StoredSalt = Convert.FromBase64String(user.PasswordSalt)
            });

            if (!isValidPassword)
            {
                throw new Exception("Hatalı giriş yaptınız.");
            }
        }
        else
        {
            throw new Exception("Hatalı giriş yaptınız.");
        }

        var userData = new UserDataDto()
        {
            UserId = user.Id,
            Token = await _authService.GenerateToken(user),
            RefreshToken = await _authService.GenerateRefreshToken(),
            ExpireDate = DateTime.Now.AddMinutes(_authSettings.ExpirationMinutes)
        };

        await _userTokenService.Create(new UserTokenDto()
        {
            UserId = user.Id,
            Token = userData.Token,
            ExpireDate = userData.ExpireDate
        });

        _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieNames.UserData, AesHelper.Encrypt(JsonSerializer.Serialize(userData)), new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = userData.ExpireDate,
            SameSite = SameSiteMode.None
        });

        return userData;
    }

    public async Task<bool> ForgetPasswordOperation(string email)
    {
        bool result = false;

        try
        {
            var userData = await _userDal.Get(new UserGetRequestDto() { Email = email });
            if (userData != null)
            {
                var forgetPassResult = await _userDal.UpdateForgetPassword(email);
                if (forgetPassResult != null && !string.IsNullOrEmpty(forgetPassResult.ForgetPasswordKey))
                {
                    string domainUrl = _configuration.GetSection("AppSettings:DomainUrl").Value!;
                    await _emailService.Send(new EmailSendModelDto()
                    {
                        Subject = "Şifremi Sıfırla | Monster Poker",
                        TemplateName = "MailResetPassword",
                        Email = userData.Email,
                        Body = $"<p>Şifrenizi sıfırlamak için <a href=\"{domainUrl}/auth/reset-password/{forgetPassResult.ForgetPasswordKey}\">buraya</a> tıklayınız.</p>"
                    });

                    result = true;
                }
            }
        }
        catch (Exception ex)
        {
            _nLogService.LogError("UpdateForgetPassword işleminde bir hata oluştu.", ex);
        }

        return result;
    }
}