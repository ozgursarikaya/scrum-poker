using ScrumPoker.Web.Models;

namespace ScrumPoker.Web.TempData
{
    public static class StaticData
    {
        public static void  CreateTempTaskList()
        {
            TaskList = new List<PlanningPokerTaskModel>()
            {
                new PlanningPokerTaskModel() { OrderNo = 1, Id = Guid.NewGuid().ToString(), Number = 133,  Title = "Lugas Safe Server IOC Operasyonları", TeamPrefix = "PAY-", IsVoted = true },
                new PlanningPokerTaskModel() { OrderNo = 2, Id = Guid.NewGuid().ToString(), Number = 134,  Title = "Reverification Geliştirmeleri", TeamPrefix = "CUS-" },
                new PlanningPokerTaskModel() { OrderNo = 3, Id = Guid.NewGuid().ToString(), Number = 135,  Title = "Bilgi Güncelleme Ekranı Geliştirmeleri", TeamPrefix = "CUS-" },
            };
        }
        public static List<PlanningPokerRoomCreateModel> RoomList { get; set; } = new();
        public static List<PlanningPokerTaskModel> TaskList { get; set; } = new();
        public static List<PlanningPokerUserTaskModel> UserVotedTaskList { get; set; } = new();
    }
}
