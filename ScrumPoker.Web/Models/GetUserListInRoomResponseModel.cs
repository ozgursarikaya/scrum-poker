namespace ScrumPoker.Web.Models
{
    public class GetUserListInRoomResponseModel
    {
        public bool IsAdminOpenedCards { get; set; }
        public List<PokerUserModel> UserList { get; set; } = new();
        public bool NextRound { get; set; }
    }
}
