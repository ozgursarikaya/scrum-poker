using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract;

public interface IUserService
{
    Task<Guid> Create(CreateUserDto request);
    Task<bool> Update(UpdateUserDto request);
}