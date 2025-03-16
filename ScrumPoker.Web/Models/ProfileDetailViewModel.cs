using ScrumPoker.Dto;

namespace ScrumPoker.Web.Models
{
	public class ProfileDetailViewModel
	{
		public ProfileDetailViewModel()
		{
			User = new UserDto();
			ChangePassword = new ChangePasswordRequestDto();
		}
		public UserDto User { get; set; }
		public ChangePasswordRequestDto ChangePassword { get; set; }
	}
}
