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

    public async Task<UserDto> Get(UserGetRequestDto request)
    {
        return await _dataProvider.Get<UserDto>(StoredProcedureNames.GetUser, request);
    }

    public async Task<UpdateForgetPasswordResponseDto> UpdateForgetPassword(string email)
    {
        return await _dataProvider.Get<UpdateForgetPasswordResponseDto>(StoredProcedureNames.UpdateUserForgetPassword, new { Email = email });
    }

    public async Task<bool> UpdatePassword(AuthResetPasswordRequestDto request)
    {
        return await _dataProvider.Get<bool>(StoredProcedureNames.UpdateUserPassword, request);
    }

	public async Task<bool> ProfileSave(ProfileSaveRequestDto request)
	{
		return await _dataProvider.Update(StoredProcedureNames.UpdateUserProfile, request);
	}

	public async Task<bool> ChangePassword(ChangePasswordRequestDto request)
	{
		return await _dataProvider.Update(StoredProcedureNames.ChangeUserPassword, new { Id = request.Id, PasswordHash = request.NewPasswordHash, PasswordSalt = request.NewPasswordSalt });
	}
}