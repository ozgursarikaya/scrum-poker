namespace ScrumPoker.Dto
{
    public class VerifyPasswordRequestDto
    {
        public string Password { get; set; }
        public byte[] StoredHash { get; set; }
        public byte[] StoredSalt { get; set; }
    }
}
