using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Web.Models;

namespace ScrumPoker.Web.Controllers
{
    public class ProfileController : Controller
    {
        [Route("profile", Name = "ProfileDetail")]
        public IActionResult Detail()
        {
			ProfileDetailViewModel vm = new ProfileDetailViewModel();

			return View(vm);
        }
    }
}
