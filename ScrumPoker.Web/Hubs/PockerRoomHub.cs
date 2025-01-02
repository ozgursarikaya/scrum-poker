using Microsoft.AspNetCore.SignalR;
using ScrumPoker.Web.Models;

namespace ScrumPoker.Web.Hubs
{
    public class PockerRoomHub : Hub
    {
        public async Task SendVote(PockerRoomHubSendVoteModel model)
        {
            await Clients.All.SendAsync("ReceiveVote", model);
        }
    }
}
