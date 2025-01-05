namespace ScrumPoker.Web.Models
{
    public class PokerUserModel
    {
        public Guid UserId { get; set; }
        public string RoomId { get; set; }
        public string UserName { get; set; } = "Anonymous";

        public PokerUserModel(string roomId, string userName)
        {
            RoomId = roomId;
            UserName = userName;
        }
    }
}
