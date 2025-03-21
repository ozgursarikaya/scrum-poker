﻿using Microsoft.AspNetCore.SignalR;
using ScrumPoker.Web.Models;
using ScrumPoker.Web.TempData;

namespace ScrumPoker.Web.Hubs
{
    public class PokerRoomHub : Hub
    {
        public static List<PokerUserModel> UserList { get; set; } = new();
        public async Task SendVote(PokerRoomHubSendVoteModel model)
        {
            model.Cid = Context.ConnectionId;
            var userModel = UserList.FirstOrDefault(w => w.UserId == model.UserId);
            if (userModel != null)
            {
                userModel.VotePoint = model.VotePoint;

                StaticData.UserVotedTaskList.Add(new PlanningPokerUserTaskModel() { RoomId = model.RoomId, TaskId = model.TaskId, UserId = model.UserId });
            }
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

        public async Task GetUserListInRoom(string roomId, bool isAdminOpenedCards, bool resetAllVotes, string taskId = "")
        {
            GetUserListInRoomResponseModel model = new GetUserListInRoomResponseModel();
            model.IsAdminOpenedCards = isAdminOpenedCards;
            model.UserList = UserList.Where(w => w.RoomId == roomId).ToList();
            model.NextRound = resetAllVotes;

            if(isAdminOpenedCards)
            {
                var task = StaticData.TaskList.FirstOrDefault(w => w.Id == taskId);
                task.IsVoted = true;
            }

            if (resetAllVotes)
            {
                model.UserList.ForEach(w => w.VotePoint = (byte)0);
            }

            await Clients.Group(roomId).SendAsync("ReceiveUserListInRoom", model).ConfigureAwait(true);
        }

        public async Task GetTaskList(string roomId)
        {
            await Clients.Group(roomId).SendAsync("ReceiveTaskList", StaticData.TaskList.OrderBy(w => w.IsVoted).ThenBy(w => w.OrderNo).ToList()).ConfigureAwait(true);
        }

        public async Task SaveProfile(PokerUserModel model)
        {
            var user = UserList.FirstOrDefault(w => w.UserId == model.UserId);
            if (user != null)
            {
                user.UserName = model.UserName;
            }
            else
            {
                UserList.Add(model);
            }
            await GetUserListInRoom(model.RoomId, false, false);
        }
    }

    class GetUserListInRoomResponseModel
    {
        public bool IsAdminOpenedCards { get; set; }
        public List<PokerUserModel> UserList { get; set; } = new();
        public bool NextRound { get; set; }
    }
}
