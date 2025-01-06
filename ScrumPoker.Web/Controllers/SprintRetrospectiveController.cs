using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    [Route("sprint-retrospective")]
    public class SprintRetrospectiveController : Controller
    {
        [Route("list", Name = "SprintRetrospectiveIndex")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
