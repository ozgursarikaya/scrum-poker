namespace ScrumPoker.Dto;

public class UpdatePlanningPokerUserDto
{
    public Guid PlanningPokerId { get; set; }
    public Guid UserId { get; set; }
    public bool? IsAdmin { get; set; }
    public bool? IsObserver { get; set; }
}