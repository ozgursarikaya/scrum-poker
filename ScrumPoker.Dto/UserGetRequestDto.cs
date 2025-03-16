namespace ScrumPoker.Dto;

public class UserGetRequestDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? ForgetPasswordKey { get; set; }
}