namespace ScrumPoker.Dto
{
    public class AuthResetPasswordRequestDto
    {
        public string ForgetPasswordKey { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
    }
}
