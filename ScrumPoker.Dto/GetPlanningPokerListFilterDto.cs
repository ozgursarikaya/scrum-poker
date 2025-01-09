namespace ScrumPoker.Dto;

public class GetPlanningPokerListFilterDto
{
    public Guid UserId { get; set; }
    public string? SearchText { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}