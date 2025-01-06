using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    [Route("report")]
    public class ReportController : Controller
    {
        [Route("planing-poker-reports", Name = "PlaningPokerReport")]
        public IActionResult PlaningPokerReport()
        {
            return View();
        }
        [Route("sprint-retrospective-reports", Name = "SprintRetrospectiveReport")]
        public IActionResult SprintRetrospectiveReport()
        {
            return View();
        }
    }
}
