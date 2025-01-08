namespace ScrumPoker.Dto;

public class CreateRetroColumnDto
{
    public Guid RetroId { get; set; }
    public string Name { get; set; } = default!;
    public string? Icon { get; set; }
    public string? Color { get; set; }
    public int OrderNo { get; set; }
}