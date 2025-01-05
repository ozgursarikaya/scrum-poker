namespace ScrumPoker.Web.Models
{
    public class PokerRoomIndexViewModel
    {
        public string RoomId { get; set; } = Guid.NewGuid().ToString();
    }
}
