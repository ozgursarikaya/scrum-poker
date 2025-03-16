namespace ScrumPoker.Dto
{
    public class CreatePasswordResponseDto
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
