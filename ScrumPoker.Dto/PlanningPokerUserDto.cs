namespace ScrumPoker.Dto;

public class PlanningPokerUserDto
{
    public Guid Id { get; set; }
    public Guid PlanningPokerId { get; set; }
    public Guid UserId { get; set; }
    public bool? IsAdmin { get; set; }
    public bool? IsObserver { get; set; }
}