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
        [Route("create", Name = "SprintRetrospectiveCreate")]
        public IActionResult Create()
        {
            return View();
        }
        [Route("retro", Name = "SprintRetrospectiveRetro")]
        public IActionResult Retro()
        {
            return View();
        }
    }
}
