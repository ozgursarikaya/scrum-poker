using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Abstract;

public interface IUserVerificationDal
{
    Task<Guid> Create(CreateUserVerificationDto request);
    Task<bool> VerifyCode(VerifyUserVerificationCodeDto request);
}