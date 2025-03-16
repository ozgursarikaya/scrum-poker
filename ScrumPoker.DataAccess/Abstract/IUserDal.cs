using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Abstract;

public interface IUserDal
{
    Task<Guid> Create(CreateUserDto request);
    Task<bool> Update(UpdateUserDto request);
    Task<UserDto> Get(UserGetRequestDto request);
    Task<UpdateForgetPasswordResponseDto> UpdateForgetPassword(string email);
    Task<bool> UpdatePassword(AuthResetPasswordRequestDto request);
}