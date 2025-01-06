using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Business.Abstract;

namespace ScrumPoker.Web.Controllers
{   
    [Route("planning-poker")]
    public class PlanningPokerController : Controller
    {
        private readonly IPlanningPokerVotingTypeService _planningPokerVotingTypeService;

        public PlanningPokerController(IPlanningPokerVotingTypeService planningPokerVotingTypeService)
        {
            _planningPokerVotingTypeService = planningPokerVotingTypeService;
        }

        [Route("list", Name = "PlanningPokerIndex")]
        public async Task<IActionResult> Index()
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
