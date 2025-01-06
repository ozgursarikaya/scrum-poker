using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
