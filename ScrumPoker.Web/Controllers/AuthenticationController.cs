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
        private readonly IAvatarService _avatarService;

		public AuthenticationController(IUserService userService, IAvatarService avatarService)
        {
            _userService = userService;
			_avatarService = avatarService;
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

		[HttpPost]
		[Route("login", Name = "AuthenticationLoginPost")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(AuthenticationLoginViewModel vm)
		{
			try
			{
                if (!ModelState.IsValid)
                {
                    SetException(new Exception("Enter your email and password."));
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
                    SetException(new Exception("You have entered incorrect data."));
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
			AuthenticationSignUpViewModel vm = new AuthenticationSignUpViewModel();
            vm.AvatarList = await _avatarService.GetList();
			return View(vm);
		}

		[HttpPost]
		[Route("signup", Name = "AuthenticationSignUpPost")]
		public async Task<IActionResult> SignUp(AuthenticationSignUpViewModel vm)
		{
            if (ModelState.IsValid)
            {
                return RedirectToRoute("DashboardIndex");
            }
			vm.AvatarList = await _avatarService.GetList();
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
                SetException(new Exception("Please enter your email address."));
                return View(vm);
            }

            bool result = await _userService.ForgetPasswordOperation(vm.Email);
            if (!result)
            {
                SetException(new Exception("An error occurred. Please try again."));
                return View(vm);
            }
            else
            {
                SetUiMessage(true, "To reset your password, you can click on the password reset link sent to your email address.");
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
                SetException(new Exception("You cannot perform this action because your password reset link has expired. You can send a forgot password request again."));
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
                    PasswordHash = Convert.ToBase64String(newPassword.Item1),
                    PasswordSalt = Convert.ToBase64String(newPassword.Item2)
                });

                if (!resetResult)
                    hasError = true;
            }
            else
                hasError = true;

            if (!hasError)
            {
                SetUiMessage(true, "Password reset process is successful. You can log in with your new password.");
                return RedirectToAction("Login");
            }
            else
            {
                SetException(new Exception("An error occurred. Please try again."));
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
