using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cafe_Mazaj.Filters
{
    public class AdminAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isLoggedIn = context.HttpContext.Session.GetString("AdminLoggedIn");
            if (string.IsNullOrEmpty(isLoggedIn) || isLoggedIn != "true")
            {
                // Redirect to Admin area's AuthController.Login
                context.Result = new RedirectToActionResult("Login", "Auth", new { area = "Admin" });
            }
            base.OnActionExecuting(context);
        }
    }
}
