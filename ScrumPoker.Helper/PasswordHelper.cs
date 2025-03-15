using System.Security.Cryptography;
using System.Text;

namespace ScrumPoker.Helper
{
	public static class PasswordHelper
	{
		public static async Task<bool> VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
		{
			using (var hmac = new HMACSHA512(storedSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
			}
		}

		public static async Task<Tuple<byte[], byte[]>> CreatePassword(string password)
		{
			byte[] passwordSalt;
			byte[] passwordHash;

			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}

			return Tuple.Create(passwordSalt, passwordHash);
		}
	}
}
