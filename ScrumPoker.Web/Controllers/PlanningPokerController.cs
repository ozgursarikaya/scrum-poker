using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Web.Models;
using ScrumPoker.Web.TempData;
using ScrumPoker.Business.Abstract;
using System.Reflection;

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

        [HttpPost]
        [Route("create", Name = "PlanningPokerCreate")]
        public IActionResult Create(PlanningPokerRoomCreateModel model)
        {
            StaticData.RoomList.Add(model);

            return Json(new { RedirectLink = Url.RouteUrl("PlanningPokerRoom", new { roomId = model.Id }) });
        }

        [Route("room/{roomId}", Name = "PlanningPokerRoom")]
        public IActionResult Room(string roomId)
        {
            string redirectUrl = string.Empty;
            var roomData = StaticData.RoomList.FirstOrDefault(w => w.Id == roomId);

            if (roomData == null)
            {
                return RedirectToAction("Error", "Error");
            }

            PlanningPokerIndexViewModel vm = new PlanningPokerIndexViewModel();
            vm.RoomId = roomId;
            vm.Name = roomData.Name;

            if (roomData.IsPasswordProtected && !StaticData.CorrectPassword)
            {
                //TO DO: Room owner ise bu akışa girmeyecek
                return RedirectToAction("RoomSecurity", new { roomId = roomId });
            }
            else
            {
                StaticData.CorrectPassword = false;
                return View(vm);
            }
        }

        [Route("room-security/{roomId}", Name = "PlanningPokerRoomSecurity")]
        public IActionResult RoomSecurity(string roomId)
        {
            PlanningPokerRoomSecurityModel vm = new PlanningPokerRoomSecurityModel();
            vm.RoomId = roomId;

            return View(vm);
        }

        [HttpPost]
        [Route("room-security/{roomId}", Name = "PlanningPokerRoomSecurity")]
        public IActionResult RoomSecurity(PlanningPokerRoomSecurityModel model)
        {
            var roomData = StaticData.RoomList.FirstOrDefault(w => w.Id == model.RoomId);
            if (roomData == null) {
                return RedirectToAction("Error", "Error");
            }

            if(roomData.Password == model.Password)
            {
                StaticData.CorrectPassword = true;
            }

            return RedirectToAction("Room", new { roomId = model.RoomId });
        }
    }
}
