using Microsoft.AspNetCore.SignalR;
using ScrumPoker.Web.Models;

namespace ScrumPoker.Web.Hubs
{
    public class PockerRoomHub : Hub
    {
        public async Task SendVote(PockerRoomHubSendVoteModel model)
        {
            model.Cid = Context.ConnectionId;

            if (model.IsJoin)
            {
                await Join(model.RoomId).ConfigureAwait(false);
            }

            await Clients.Group(model.RoomId).SendAsync("ReceiveVote", model).ConfigureAwait(true);
        }

        public async Task Join(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId, CancellationToken.None);
        }

        public Task Disconnect(string roomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }
    }
}
