namespace ScrumPoker.Web.Models
{
    public class PlanningPokerTaskModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N").ToLower();
        public string Title { get; set; }
        public int Number { get; set; }
        public string TeamPrefix { get; set; } = "CUS-";
        public string TaskNoWithTeamPrefix
        {
            get
            {
                return TeamPrefix + Number.ToString();
            }
        }
        public int OrderNo { get; set; }
        public bool IsVoted { get; set; }
    }
}
