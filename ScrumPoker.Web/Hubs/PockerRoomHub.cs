using Microsoft.AspNetCore.SignalR;
using ScrumPoker.Web.Models;
using System.Reflection;

namespace ScrumPoker.Web.Hubs
{
    public class PockerRoomHub : Hub
    {
        public async Task SendVote(PockerRoomHubSendVoteModel model)
        {
            model.Cid = Context.ConnectionId;

            await Clients.Group(model.RoomId).SendAsync("ReceiveVote", model).ConfigureAwait(true);
        }

        public async Task Join(string roomId, string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId, CancellationToken.None);
            await Clients.Group(roomId).SendAsync("ReceiveVote", new PockerRoomHubSendVoteModel() { IsJoin = true, UserName = username }).ConfigureAwait(true);
        }

        public Task Disconnect(string roomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }
    }
}
