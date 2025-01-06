using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        [Route("list", Name = "UserIndex")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
