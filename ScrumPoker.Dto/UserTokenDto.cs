namespace ScrumPoker.Dto
{
	public class UserTokenDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string Token { get; set; }
		public DateTime ExpireDate { get; set; }
	}
}
