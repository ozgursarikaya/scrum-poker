using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract;

public interface IUserService
{
    Task<Guid> Create(CreateUserDto request);
    Task<bool> Update(UpdateUserDto request);
    Task<UserDataDto> LoginOperation(LoginOperationDto request);
    Task<bool> ForgetPasswordOperation(string email);
    Task<UserDto> Get(UserGetRequestDto request);
    Task<bool> UpdatePassword(AuthResetPasswordRequestDto request);
}