using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cafe_Mazaj.Filters
{
    public class AdminAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isLoggedIn = context.HttpContext.Session.GetString("AdminLoggedIn");
            if (string.IsNullOrEmpty(isLoggedIn))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", new { area = "" });
            }
            base.OnActionExecuting(context);
        }
    }
}
