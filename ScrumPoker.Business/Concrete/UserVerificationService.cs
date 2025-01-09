using ScrumPoker.Business.Abstract;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.Dto;
using ScrumPoker.Helper;

namespace ScrumPoker.Business.Concrete;

public class UserVerificationService : IUserVerificationService
{
    private readonly IUserVerificationDal _userVerificationDal;

    public UserVerificationService(IUserVerificationDal userVerificationDal)
    {
        _userVerificationDal = userVerificationDal;
    }

    public async Task<Guid> Create(CreateUserVerificationDto request)
    {
        request.Code = StringHelper.GenerateRandomCode();
        return await _userVerificationDal.Create(request);
    }

    public async Task<bool> VerifyCode(VerifyUserVerificationCodeDto request)
    {
        return await _userVerificationDal.VerifyCode(request);
    }
}