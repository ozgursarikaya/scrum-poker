using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract;

public interface IUserVerificationService
{
    Task<Guid> Create(CreateUserVerificationDto request);
    Task<bool> VerifyCode(VerifyUserVerificationCodeDto request);
}