using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Business.Abstract;
using ScrumPoker.Dto;
using ScrumPoker.Web.Models;

namespace ScrumPoker.Web.Controllers
{
    public class ProfileController : BaseController
	{
		private readonly ILoggerService _loggerService;
		private readonly IUserService _userService;
		public ProfileController(ILoggerService loggerService, IUserService userService)
		{
			_loggerService = loggerService;
			_userService = userService;
		}
		[Route("profile", Name = "ProfileDetail")]
        public async Task<IActionResult> Detail()
        {
			ProfileDetailViewModel vm = new ProfileDetailViewModel();
			var user = await _userService.Get(new UserGetRequestDto() { Id = Guid.NewGuid() });
			if (user != null)
			{
				vm.User.Id = user.Id;
				vm.User.Name = user.Name;
				vm.User.Avatar = user.Avatar;
				vm.User.Email = user.Email;
			}
			return View(vm);
        }
		[Route("profile-save", Name = "ProfileSave")]
		public async Task<IActionResult> ProfileSave(ProfileDetailViewModel vm)
		{
			try
			{
				var result = await _userService.ProfileSave(vm.User);
				if (result)
				{
					SetUiMessage(true);
				}
			}
			catch (Exception ex)
			{
				_loggerService.LogError("There is a problem.", ex);
				SetException(ex);
			}

			return RedirectToRoute("ProfileDetail");
		}
		[Route("change-password", Name = "ProfileChangePassword")]
		public async Task<IActionResult> ProfileChangePassword(ProfileDetailViewModel vm)
		{
			try
			{
				var result = await _userService.ChangePassword(vm.ChangePassword);
				if (result)
				{
					SetUiMessage(true);
				}
			}
			catch (Exception ex)
			{
				_loggerService.LogError("There is a problem.", ex);
				SetException(ex);
			}

			return RedirectToRoute("ProfileDetail");
		}
	}
}
