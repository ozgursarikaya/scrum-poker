using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Abstract;

public interface IUserDal
{
    Task<Guid> Create(CreateUserDto request);
    Task<bool> Update(UpdateUserDto request);
}