using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    public class DashboardController : Controller
    {
        [Route("")]
        [Route("dashboard", Name ="DashboardIndex")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
