using ScrumPoker.Dto;

namespace ScrumPoker.Web.Models
{
	public class AuthenticationSignUpViewModel
	{
		public AuthenticationSignUpViewModel()
		{
			AvatarList = new List<string>();
			SignUpUser = new SignUpUserDto();
		}
		public List<string> AvatarList { get; set; }
		public SignUpUserDto SignUpUser { get; set; }
	}
}
