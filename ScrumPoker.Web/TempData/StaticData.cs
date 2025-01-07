using ScrumPoker.Web.Models;

namespace ScrumPoker.Web.TempData
{
    public static class StaticData
    {
        public static List<PlanningPokerRoomCreateModel> RoomList { get; set; } = new();
    }
}
