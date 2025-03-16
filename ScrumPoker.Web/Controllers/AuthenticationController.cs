using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScrumPoker.Business.Abstract;
using ScrumPoker.Dto;
using ScrumPoker.Helper;
using ScrumPoker.Web.Models;

namespace ScrumPoker.Web.Controllers
{
    public class AuthenticationController : BaseController
    {
		private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

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
                if (!ModelState.IsValid)
                {
                    SetException(new Exception("Email ve şifrenizi giriniz."));
                    return View(vm);
                }

				var userLoginData = await _userService.LoginOperation(new Dto.LoginOperationDto()
				{
					Email = vm.Email,
					Password = vm.Password,
					RememberMe = vm.RememberMe
				});
                if (userLoginData == null)
                {
                    SetException(new Exception("Email veya şifreniz hatalı."));
                    return View(vm);
                }

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
                SetException(ex);
            }

			return RedirectToRoute("DashboardIndex");
		}

		[Route("signup", Name = "AuthenticationSignUp")]
		public async Task<IActionResult> SignUp()
		{
			var numberList = RandomHelper.GetRandomNumbers(1, 30, 12);
			AuthenticationSignUpViewModel vm = new AuthenticationSignUpViewModel();
			foreach (var number in numberList) 
			{
				vm.AvatarList.Add("../images/avatars/avatar-" + number.ToString() + ".jpg");
			}
			return View(vm);
		}

        [Route("forgot-password", Name = "AuthenticationForgotPassword")]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("forget-password")]
        public async Task<IActionResult> ForgotPassword(AuthForgetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                SetException(new Exception("Email adresinizi giriniz."));
                return View(vm);
            }

            bool result = await _userService.ForgetPasswordOperation(vm.Email);
            if (!result)
            {
                SetException(new Exception("Bir hata oluştu. Lütfen yeniden deneyiniz."));
                return View(vm);
            }
            else
            {
                SetUiMessage(true, "Şifrenizi sıfırlamak için email adresinize gönderilen şifre sıfırlama linkine tıklayabilirsiniz.");
                return RedirectToAction("Login");
            }
        }

        [Route("reset-password/{key}", Name = "AuthenticationResetPassword")]
        public async Task<IActionResult> ResetPassword(string key)
        {
            AuthResetPasswordViewModel vm = new AuthResetPasswordViewModel()
            {
                ForgetPasswordKey = key
            };

            var userData = await _userService.Get(new UserGetRequestDto() { ForgetPasswordKey = key });
            if (userData != null)
            {
                return View(vm);
            }
            else
            {
                SetException(new Exception("Şifre sıfırlama linkinizin süresi dolduğu için bu işlemi gerçekleştiremezsiniz. Tekrar şifremi unuttum talebi gönderebilirsiniz."));
                return View(vm);
            }
        }

        [HttpPost]
        [Route("reset-password/{key}", Name = "AuthenticationResetPassword")]
        public async Task<IActionResult> ResetPassword(AuthResetPasswordViewModel vm)
        {
            bool hasError = false;

            if (!string.IsNullOrEmpty(vm.ForgetPasswordKey))
            {
                var newPassword = await PasswordHelper.CreatePassword(vm.NewPassword);
                bool resetResult = await _userService.UpdatePassword(new AuthResetPasswordRequestDto()
                {
                    ForgetPasswordKey = vm.ForgetPasswordKey,
                    PasswordHash = Convert.ToBase64String(newPassword.PasswordHash),
                    PasswordSalt = Convert.ToBase64String(newPassword.PasswordSalt)
                });

                if (!resetResult)
                    hasError = true;
            }
            else
                hasError = true;

            if (!hasError)
            {
                SetUiMessage(true, "Şifre sıfırlama işlemi başarılı. Yeni şifreniz ile giriş yapabilirsiniz.");
                return RedirectToAction("Login");
            }
            else
            {
                SetException(new Exception("Bir hata oluştu. Lütfen yeniden deneyiniz."));
                return View(vm);
            }
        }

        [Route("logout", Name = "AuthenticationLogout")]
		public async Task<IActionResult> Logout()
		{
			Response.Cookies.Delete("UserLoginData");
			return View();
		}
	}
}
