using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    [Route("team")]
    public class TeamController : Controller
    {
        [Route("list", Name = "TeamIndex")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
