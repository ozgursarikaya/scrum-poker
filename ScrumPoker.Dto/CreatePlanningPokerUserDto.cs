namespace ScrumPoker.Dto;

public class CreatePlanningPokerUserDto
{
    public Guid PlanningPokerId { get; set; }
    public Guid UserId { get; set; }
    public bool IsAdmin { get; set; }
}