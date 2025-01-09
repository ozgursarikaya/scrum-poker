using ScrumPoker.Common.Consts;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.DataProvider;
using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Concrete;

public class UserDal : IUserDal
{
    private readonly IDataProvider _dataProvider;

    public UserDal(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<Guid> Create(CreateUserDto request)
    {
        return await _dataProvider.Insert<Guid>(StoredProcedureNames.CreateUser, request);
    }

    public async Task<bool> Update(UpdateUserDto request)
    {
        return await _dataProvider.Update(StoredProcedureNames.UpdateUser, request);
    }
}