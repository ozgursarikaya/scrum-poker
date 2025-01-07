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
            HttpContext.Session.SetString("IsOwner", "true");
            
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
                return Redirect("/error");
            }

            PlanningPokerIndexViewModel vm = new PlanningPokerIndexViewModel();
            vm.RoomId = roomId;
            vm.Name = roomData.Name;
            
            bool isOwner = HttpContext.Session.GetString("IsOwner") != null ? bool.Parse(HttpContext.Session.GetString("IsOwner")) : false;
            bool correctPassword = HttpContext.Session.GetString("CorrectPassword") != null ? bool.Parse(HttpContext.Session.GetString("CorrectPassword")) : false;

            if (roomData.IsPasswordProtected && !correctPassword && !isOwner)
            {
                //TO DO: Room owner ise bu akışa girmeyecek
                return RedirectToAction("RoomSecurity", new { roomId = roomId });
            }
            else
            { 
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
                return Redirect("/error");
            }

            if(roomData.Password == model.Password)
            {
                HttpContext.Session.SetString("CorrectPassword", "true");

                return RedirectToAction("Room", new { roomId = model.RoomId });
            }

            return Redirect("/error");
        }
    }
}
