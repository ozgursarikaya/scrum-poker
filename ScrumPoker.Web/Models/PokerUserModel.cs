namespace ScrumPoker.Web.Models
{
    public class PokerUserModel
    {
        public string UserId { get; set; }
        public string RoomId { get; set; }
        public string UserName { get; set; } = "Anonymous";

        public PokerUserModel(string roomId, string userId)
        {
            RoomId = roomId;
            UserId = userId;
        }
    }
}
