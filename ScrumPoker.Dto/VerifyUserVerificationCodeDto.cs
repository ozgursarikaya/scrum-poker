namespace ScrumPoker.Dto;

public class VerifyUserVerificationCodeDto
{
    public Guid UserId { get; set; }
    public string Code { get; set; } = default!;
}