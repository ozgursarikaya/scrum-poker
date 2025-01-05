namespace ScrumPoker.Web.Models
{
    public class PokerRoomHubSendVoteModel
    {
        public string UserName { get; set; }
        public byte VotePoint { get; set; }
        public string Cid { get; set; }
        public string RoomId { get; set; }
        public bool IsJoin { get; set; }
    }
}
