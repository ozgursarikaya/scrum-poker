using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract
{
    public interface IAuthService
    {
        Task<string> GenerateToken(UserDto user);
        Task<string> GenerateRefreshToken();
    }
}
