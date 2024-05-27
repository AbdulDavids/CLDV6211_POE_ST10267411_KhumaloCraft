using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CLDV6211_POE_ST10267411_KhumaloCraft.Models;
using CLDV6211_POE_ST10267411_KhumaloCraft.Data;

namespace CLDV6211_POE_ST10267411_KhumaloCraft.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string userName, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserID", user.UserID.ToString());
                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Index", "Product");
            }

            ViewBag.ErrorMessage = "Invalid login attempt.";
            return View();
        }
    }
}
