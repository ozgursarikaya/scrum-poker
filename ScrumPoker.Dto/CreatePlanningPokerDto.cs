namespace ScrumPoker.Dto;

public class CreatePlanningPokerDto
{
    public Guid? UserId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public short PlanningPokerVotingTypeId { get; set; }
    public bool IsAnonymousVoting { get; set; }
    public string? PasswordProtected { get; set; }
    public Guid? CreatedBy { get; set; }
}