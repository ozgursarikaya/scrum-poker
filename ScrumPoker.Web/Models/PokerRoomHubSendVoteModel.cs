namespace ScrumPoker.Web.Models
{
    public class PokerRoomHubSendVoteModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public byte VotePoint { get; set; }
        public string Cid { get; set; }
        public string RoomId { get; set; }
        public bool IsJoin { get; set; }
        public string TaskId { get; set; }
    }
}
