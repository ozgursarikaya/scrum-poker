using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Web.Models;
using Microsoft.AspNetCore.SignalR;
using ScrumPoker.Web.Hubs;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ScrumPoker.Web.Controllers
{
    [Route("room")]
    public class PockerRoomController : Controller
    {
        private readonly IHubContext<PockerRoomHub> _hubContext;

        public PockerRoomController(IHubContext<PockerRoomHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task<IActionResult> Index(string roomId)
        {
            PockerRoomIndexViewModel vm = new PockerRoomIndexViewModel();
            vm.RoomId = roomId;
            var model = new PockerRoomHubSendVoteModel() { IsJoin = false, RoomId = roomId, VotePoint = 99, UserName = "U-" + Guid.NewGuid().ToString("N") };

            //var connection = new HubConnectionBuilder().WithUrl("https://localhost:7205/roomHub").Build();

            //await connection.StartAsync();

            //await connection.SendAsync("Join", roomId);

            //await connection.SendAsync("ReceiveVote", new PockerRoomHubSendVoteModel() { IsJoin = false, RoomId = roomId, VotePoint = 99, UserName = "U-" + Guid.NewGuid().ToString("N") });
            
            //await _hubContext.Clients.Group(roomId).SendAsync("ReceiveVote", model);

            return View(vm);
        }
    }
}
