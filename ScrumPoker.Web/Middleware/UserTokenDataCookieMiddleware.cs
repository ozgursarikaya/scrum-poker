using System.Text.Json;
using ScrumPoker.Common.Consts;
using ScrumPoker.Dto;
using ScrumPoker.Helper;

namespace ScrumPoker.Web.Middleware
{
	public class UserTokenDataCookieMiddleware
	{
		private readonly RequestDelegate _next;

		public UserTokenDataCookieMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var cookie = context.Request.Cookies[CookieNames.UserData];

			if (!string.IsNullOrEmpty(cookie))
			{
				var userTokenData = JsonSerializer.Deserialize<UserDataDto>(AesHelper.Decrypt(cookie));
				context.Request.Headers.Append("Authorization", $"Bearer {userTokenData.Token}");
			}

			await _next(context);
		}
	}
}
