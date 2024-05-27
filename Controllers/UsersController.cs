using Microsoft.AspNetCore.Mvc;
using CLDV6211_POE_ST10267411_KhumaloCraft.Models;
using System.Linq;
using CLDV6211_POE_ST10267411_KhumaloCraft.Data;

namespace CLDV6211_POE_ST10267411_KhumaloCraft.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.UserName == user.UserName))
                {
                    ModelState.AddModelError("UserName", "Username is already taken.");
                    return View(user);
                }

                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Login");
            }
            return View(user);
        }
    }
}
