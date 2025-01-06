using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    public class ProfileController : Controller
    {
        [Route("profile", Name = "ProfileDetail")]
        public IActionResult Detail()
        {
            return View();
        }
    }
}
