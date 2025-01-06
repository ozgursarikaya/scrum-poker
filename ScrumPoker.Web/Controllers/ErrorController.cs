using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
