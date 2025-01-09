using ScrumPoker.Business.Abstract;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.Dto;

namespace ScrumPoker.Business.Concrete;

public class UserService : IUserService
{
    private readonly IUserDal _userDal;

    public UserService(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public async Task<Guid> Create(CreateUserDto request)
    {
        return await _userDal.Create(request);
    }

    public async Task<bool> Update(UpdateUserDto request)
    {
        return await _userDal.Update(request);
    }
}