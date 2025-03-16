namespace ScrumPoker.Dto
{
	public class AuthSettingsDto
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public int ExpirationMinutes { get; set; }
	}
}
