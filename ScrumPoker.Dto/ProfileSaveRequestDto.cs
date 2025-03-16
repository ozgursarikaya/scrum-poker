namespace ScrumPoker.Dto
{
	public class ProfileSaveRequestDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Avatar { get; set; }
	}
}
