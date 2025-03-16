using ScrumPoker.Dto;

namespace ScrumPoker.Web.Models
{
	public class ProfileDetailViewModel
	{
		public ProfileDetailViewModel()
		{
			User = new ProfileSaveRequestDto();
			ChangePassword = new ChangePasswordRequestDto();
		}
		public ProfileSaveRequestDto User { get; set; }
		public ChangePasswordRequestDto ChangePassword { get; set; }
	}
}
