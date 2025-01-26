namespace ScrumPoker.Web.Models
{
    public class PlanningPokerUserTaskModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N").ToLower();
        public string RoomId { get; set; }
        public string TaskId { get; set; }
        public string UserId { get; set; }
    }
}
