namespace ScrumPoker.Dto;

public class GetPlanningPokerListDto
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public short PlanningPokerVotingTypeId { get; set; }
    public bool IsAnonymousVoting { get; set; }
    public string? PasswordProtected { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid? ModifiedBy { get; set; }
    public bool IsActive { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public byte RoundCount { get; set; }
}