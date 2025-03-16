using System.Security.Cryptography;
using System.Text;
using ScrumPoker.Dto;

namespace ScrumPoker.Helper
{
	public static class PasswordHelper
	{
        public static async Task<bool> VerifyPassword(VerifyPasswordRequestDto request)
        {
            using (var hmac = new HMACSHA512(request.StoredSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
                return CryptographicOperations.FixedTimeEquals(computedHash, request.StoredHash);
            }
        }

        public static async Task<CreatePasswordResponseDto> CreatePassword(string password)
        {
            CreatePasswordResponseDto result = new CreatePasswordResponseDto();

            using (var hmac = new HMACSHA512())
            {
                result.PasswordSalt = hmac.Key;
                result.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            return result;
        }
    }
}
