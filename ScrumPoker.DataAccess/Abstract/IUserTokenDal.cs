using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Abstract
{
	public interface IUserTokenDal
	{
		Task<UserTokenDto> Get(string token);
		Task<Guid> Create(UserTokenDto data);
	}
}
