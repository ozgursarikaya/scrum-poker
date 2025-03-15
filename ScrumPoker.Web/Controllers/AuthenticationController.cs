using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScrumPoker.Helper;
using ScrumPoker.Web.Models;

namespace ScrumPoker.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        [Route("login", Name = "AuthenticationLogin")]
        public async Task<IActionResult> Login()
        {
            AuthenticationLoginViewModel vm = new AuthenticationLoginViewModel();

			var cookieValue = Request.Cookies["UserLoginData"];
			if (!string.IsNullOrEmpty(cookieValue))
			{
				vm = JsonConvert.DeserializeObject<AuthenticationLoginViewModel>(AesHelper.Decrypt(cookieValue));
			}

			return View(vm);
        }
		[Route("login", Name = "AuthenticationLoginPost")]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<IActionResult> Login(AuthenticationLoginViewModel vm)
		{
			try
			{

				//login işlemleri

				if (vm.RememberMe)
				{
					var cookieValue = JsonConvert.SerializeObject(vm);
					var cookieOptions = new CookieOptions
					{
						Expires = DateTime.Now.AddDays(30),
						HttpOnly = true, 
						IsEssential = true
					};
					Response.Cookies.Append("UserLoginData", AesHelper.Encrypt(cookieValue), cookieOptions);
				}
			}
			catch (Exception ex)
			{
				
			}

			return RedirectToRoute("AuthenticationLogin");
		}



		[Route("signup", Name = "AuthenticationSignUp")]
		public async Task<IActionResult> SignUp()
		{
			return View();
		}
        [Route("forgot-password", Name = "AuthenticationForgotPassword")]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

		[Route("logout", Name = "AuthenticationLogout")]
		public async Task<IActionResult> Logout()
		{
			Response.Cookies.Delete("UserLoginData");
			return View();
		}
	}
}
