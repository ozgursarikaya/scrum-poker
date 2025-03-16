using ScrumPoker.Business.Abstract;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.Dto;

namespace ScrumPoker.Business.Concrete
{
	public class UserTokenService : IUserTokenService
	{
		private readonly IUserTokenDal _userTokenDal;

		public UserTokenService(IUserTokenDal userTokenDal)
		{
			_userTokenDal = userTokenDal;
		}

		public async Task<UserTokenDto> Get(string token)
		{
			return await _userTokenDal.Get(token);
		}
		public async Task<Guid> Create(UserTokenDto data)
		{
			return await _userTokenDal.Create(data);
		}
	}
}
