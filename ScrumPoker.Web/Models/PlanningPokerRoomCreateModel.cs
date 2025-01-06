namespace ScrumPoker.Web.Models
{
    public class PlanningPokerRoomCreateModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N").ToLower();
        public string Name { get; set; }
        public bool IsPasswordProtected { get; set; }
        public string Password { get; set; }
        public string OwnerUserId { get; set; }
    }
}
