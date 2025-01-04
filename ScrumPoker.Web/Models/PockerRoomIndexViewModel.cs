namespace ScrumPoker.Web.Models
{
    public class PockerRoomIndexViewModel
    {
        public string RoomId { get; set; } = Guid.NewGuid().ToString();
    }
}
