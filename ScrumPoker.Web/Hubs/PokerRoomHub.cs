using Microsoft.AspNetCore.SignalR;
using ScrumPoker.Web.Models;

namespace ScrumPoker.Web.Hubs
{
    public class PokerRoomHub : Hub
    {
        public static List<PokerUserModel> UserList { get; set; } = new();
        public async Task SendVote(PokerRoomHubSendVoteModel model)
        {
            model.Cid = Context.ConnectionId;

            await Clients.Group(model.RoomId).SendAsync("ReceiveVote", model).ConfigureAwait(true);
        }

        public async Task Join(string roomId, string username)
        {
            UserList.Add(new PokerUserModel(roomId,username));

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId, CancellationToken.None);
            await Clients.Group(roomId).SendAsync("ReceiveVote", new PokerRoomHubSendVoteModel() { IsJoin = true, UserName = username }).ConfigureAwait(true);
        }

        public Task Disconnect(string roomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            
        }

        public async Task GetUserListInRoom(string roomId)
        {
            await Clients.Group(roomId).SendAsync("ReceiveUserListInRoom", UserList.Where(w => w.RoomId == roomId)).ConfigureAwait(true);
        }

    }
}
