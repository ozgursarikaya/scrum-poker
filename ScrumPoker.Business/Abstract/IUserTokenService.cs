using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract
{
	public interface IUserTokenService
	{
		Task<UserTokenDto> Get(string token);
		Task<Guid> Create(UserTokenDto data);
	}
}
