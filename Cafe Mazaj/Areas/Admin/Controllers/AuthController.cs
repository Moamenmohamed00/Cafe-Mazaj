using Microsoft.AspNetCore.Mvc;

namespace Cafe_Mazaj.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config) => _config = config;

        [HttpGet]
        public IActionResult Login()
        {
            // If already logged in, go to dashboard directly
            if (HttpContext.Session.GetString("AdminLoggedIn") == "true")
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            var adminUsername = _config["AdminSettings:Username"];
            var adminPassword = _config["AdminSettings:Password"];

            if (username == adminUsername && password == adminPassword)
            {
                HttpContext.Session.SetString("AdminLoggedIn", "true");
                HttpContext.Session.SetString("AdminUsername", username);
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            ViewBag.Error = "Invalid credentials. Please try again.";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth", new { area = "Admin" });
        }
    }
}
