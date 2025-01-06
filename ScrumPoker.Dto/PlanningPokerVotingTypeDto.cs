namespace ScrumPoker.Dto;

public class PlanningPokerVotingTypeDto
{
    public short Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public short OrderNo { get; set; }
    public bool IsActive { get; set; }
}