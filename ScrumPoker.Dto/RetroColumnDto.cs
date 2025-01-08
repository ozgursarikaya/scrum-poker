namespace ScrumPoker.Dto;

public class RetroColumnDto
{
    public Guid Id { get; set; }
    public Guid RetroId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Color { get; set; }
    public int OrderNo { get; set; }
}