namespace ScrumPoker.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
}