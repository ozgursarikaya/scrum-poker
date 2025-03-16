namespace ScrumPoker.Dto
{
	public class ChangePasswordRequestDto
	{
		public string OldPassword { get; set; }
		public string NewPassword1 { get; set; }
		public string NewPassword2 { get; set; }
	}
}
