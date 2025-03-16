using System.ComponentModel.DataAnnotations;

namespace ScrumPoker.Web.Models
{
    public class AuthForgetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
