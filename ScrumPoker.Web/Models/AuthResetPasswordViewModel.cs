using System.ComponentModel.DataAnnotations;

namespace ScrumPoker.Web.Models
{
    public class AuthResetPasswordViewModel
    {
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public string ForgetPasswordKey { get; set; }
        public bool IsSuccess { get; set; }
    }
}
