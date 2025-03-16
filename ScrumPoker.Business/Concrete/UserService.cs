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
            bool isValidPassword = await PasswordHelper.VerifyPassword(request.Password, Convert.FromBase64String(user.PasswordSalt), Convert.FromBase64String(user.PasswordHash));

            if (!isValidPassword)
            {
                throw new Exception("You have entered incorrect data.");
            }
        }
        else
        {
            throw new Exception("You have entered incorrect data.");
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
                        Subject = "Reset Password | Monster Poker",
                        TemplateName = "MailResetPassword",
                        Email = userData.Email,
                        Body = $"<p>Click <a href=\"{domainUrl}/auth/reset-password/{forgetPassResult.ForgetPasswordKey}\">here</a> to reset your password.</p>"
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

    public async Task<bool> ProfileSave(ProfileSaveRequestDto request)
    {
        return await _userDal.ProfileSave(request);  
    }
	public async Task<bool> ChangePassword(ChangePasswordRequestDto request)
	{
        bool isOk = false;
		var user = await _userDal.Get(new UserGetRequestDto() { Id = request.Id });
		if (user != null)
		{
			bool isValidPassword = await PasswordHelper.VerifyPassword(request.OldPassword, Convert.FromBase64String(user.PasswordSalt), Convert.FromBase64String(user.PasswordHash));

			if (!isValidPassword)
			{
				throw new Exception("You have entered incorrect data.");
			}

			var newPassword = await PasswordHelper.CreatePassword(request.NewPassword1);
			request.NewPasswordHash = Convert.ToBase64String(newPassword.Item1);
            request.NewPasswordSalt = Convert.ToBase64String(newPassword.Item2);

			isOk = await _userDal.ChangePassword(request);

		}
		return isOk;
	}
}