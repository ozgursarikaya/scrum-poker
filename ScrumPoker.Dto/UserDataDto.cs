namespace ScrumPoker.Dto
{
    public class UserDataDto
	{
		public Guid UserId { get; set; }
        public string Token { get; set; }
		public string RefreshToken { get; set; }
		public DateTime ExpireDate { get; set; }
    }
}
