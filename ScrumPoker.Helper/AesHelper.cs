using System.Security.Cryptography;
using System.Text;

namespace ScrumPoker.Helper
{
	public class AesHelper
	{
		private const string AesIV = @"!QAZ2WSX#EDC4RFV";
		private static string AesKey = @"5TGBDYHN7UJM(IK<";
		public static string Encrypt(string text)
		{
			AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
			aes.BlockSize = 128;
			aes.KeySize = 128;
			aes.IV = Encoding.UTF8.GetBytes(AesIV);
			aes.Key = Encoding.UTF8.GetBytes(AesKey);
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;
			byte[] src = Encoding.Unicode.GetBytes(text);
			using (ICryptoTransform encrypt = aes.CreateEncryptor())
			{
				byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
				return Convert.ToBase64String(dest);
			}
		}
		public static string Decrypt(string text)
		{
			AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
			aes.BlockSize = 128;
			aes.KeySize = 128;
			aes.IV = Encoding.UTF8.GetBytes(AesIV);
			aes.Key = Encoding.UTF8.GetBytes(AesKey);
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;
			byte[] src = System.Convert.FromBase64String(text);
			using (ICryptoTransform decrypt = aes.CreateDecryptor())
			{
				byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
				return Encoding.Unicode.GetString(dest);
			}
		}
	}
}
