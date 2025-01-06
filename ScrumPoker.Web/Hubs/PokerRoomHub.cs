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

        public async Task Join(string roomId, string userId)
        {
            PokerUserModel userModel = new PokerUserModel(roomId, userId);
            if (!UserList.Any(w => w.UserId == userId))
            {
                UserList.Add(userModel);
            }
            else
            {
                userModel = UserList.FirstOrDefault(w => w.UserId == userId);
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId, CancellationToken.None);
            await Clients.Group(roomId).SendAsync("ReceiveVote", new PokerRoomHubSendVoteModel() { IsJoin = true, UserId = userId, UserName = userModel.UserName }).ConfigureAwait(true);
        }

        public Task Disconnect(string roomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task GetUserListInRoom(string roomId)
        {
            await Clients.Group(roomId).SendAsync("ReceiveUserListInRoom", UserList.Where(w => w.RoomId == roomId)).ConfigureAwait(true);
        }

        public async Task SaveProfile(PokerUserModel model)
        {
            var user = UserList.FirstOrDefault(w => w.UserId == model.UserId);
            if (user != null)
            {
                user.UserName = model.UserName;
            }
            await GetUserListInRoom(model.RoomId);
        }
    }
}
