namespace ScrumPoker.Dto;

public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Email { get; set; }
    public string? Avatar { get; set; }
}