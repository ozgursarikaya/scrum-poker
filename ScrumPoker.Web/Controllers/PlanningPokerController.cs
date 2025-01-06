using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    [Route("planning-poker")]
    public class PlanningPokerController : Controller
    {
        [Route("list", Name = "PlanningPokerIndex")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("create", Name = "PlanningPokerCreate")]
        public IActionResult Create()
        {
            return View();
        }
        [Route("room/{roomId}", Name = "PlanningPokerRoom")]
        public IActionResult Room(Guid roomId)
        {
            return View();
        }
    }
}
