using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        [Route("login", Name = "AuthenticationLogin")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("logout", Name = "AuthenticationLogout")]
        public IActionResult Logout()
        {
            return View();
        }
        [Route("forgot-password", Name = "AuthenticationForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
