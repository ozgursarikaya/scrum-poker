using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
