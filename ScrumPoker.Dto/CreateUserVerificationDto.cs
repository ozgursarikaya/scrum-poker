namespace ScrumPoker.Dto;

public class CreateUserVerificationDto
{
    public Guid UserId { get; set; }
    public string Code { get; set; } = default!;
}