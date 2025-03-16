using ScrumPoker.Common.Consts;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.DataProvider;
using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Concrete
{
	public class UserTokenDal : IUserTokenDal
	{
		private readonly IDataProvider _dataProvider;

		public UserTokenDal(IDataProvider dataProvider)
		{
			_dataProvider = dataProvider;
		}

		public async Task<UserTokenDto> Get(string token)
		{
			return await _dataProvider.Get<UserTokenDto>(StoredProcedureNames.GetUserToken, new { Token = token });
		}

		public async Task<Guid> Create(UserTokenDto data)
		{
			return await _dataProvider.Insert<Guid>(StoredProcedureNames.CreateUserToken, new { UserId = data.UserId, Token = data.Token, ExpireDate = data.ExpireDate });
		}
	}
}
