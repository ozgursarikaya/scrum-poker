using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    [Route("report")]
    public class ReportController : Controller
    {
        [Route("list", Name = "ReportIndex")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
