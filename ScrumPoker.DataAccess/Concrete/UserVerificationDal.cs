using ScrumPoker.Common.Consts;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.DataProvider;
using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Concrete;

public class UserVerificationDal : IUserVerificationDal
{
    private readonly IDataProvider _dataProvider;

    public UserVerificationDal(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<Guid> Create(CreateUserVerificationDto request)
    {
        return await _dataProvider.Insert<Guid>(StoredProcedureNames.CreateUserVerification, request);
    }

    public async Task<bool> VerifyCode(VerifyUserVerificationCodeDto request)
    {
        return await _dataProvider.Get<bool>(StoredProcedureNames.VerifyUserVerificationCode, request);
    }
}