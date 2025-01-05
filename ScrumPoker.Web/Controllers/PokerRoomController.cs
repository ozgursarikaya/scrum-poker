using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Web.Models;

namespace ScrumPoker.Web.Controllers
{
    [Route("room")]
    public class PokerRoomController : Controller
    {
        public async Task<IActionResult> Index(string roomId)
        {
            PokerRoomIndexViewModel vm = new PokerRoomIndexViewModel();
            vm.RoomId = roomId;

            return View(vm);
        }
    }
}
