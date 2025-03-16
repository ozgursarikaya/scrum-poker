using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class JwtAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        string controllerName = context.ActionDescriptor.RouteValues["controller"];
        string actionName = context.ActionDescriptor.RouteValues["action"];

        if (!user.Identity.IsAuthenticated && controllerName != "Authentication")
        {
            context.Result = new RedirectToActionResult("Login", "Authentication", null);
        }
    }
}
