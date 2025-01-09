namespace ScrumPoker.Dto;

public class CreateUserDto
{
    public string Name { get; set; } = default!;
    public string? Email { get; set; }
    public string? Avatar { get; set; }
}