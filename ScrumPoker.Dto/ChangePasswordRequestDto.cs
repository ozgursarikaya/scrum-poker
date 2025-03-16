namespace ScrumPoker.Dto
{
	public class ChangePasswordRequestDto
	{
		public Guid Id { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword1 { get; set; }
		public string NewPassword2 { get; set; }
		public string NewPasswordHash { get; set; }
		public string NewPasswordSalt { get; set; }
	}
}
