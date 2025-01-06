using ScrumPoker.Web.Models;

namespace ScrumPoker.Web.TempData
{
    public static class StaticData
    {
        public static List<PlanningPokerRoomCreateModel> RoomList { get; set; } = new();
        public static List<PokerUserModel> UserList { get; set; } = new();
        public static bool CorrectPassword { get; set; }
    }
}
