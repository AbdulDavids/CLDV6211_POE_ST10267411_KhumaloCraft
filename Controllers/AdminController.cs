using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CLDV6211_POE_ST10267411_KhumaloCraft.Models;
using CLDV6211_POE_ST10267411_KhumaloCraft.Data;

namespace CLDV6211_POE_ST10267411_KhumaloCraft.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName) || userName != "admin")
            {
                return RedirectToAction("Index", "Product");
            }

            var users = await _context.Users.ToListAsync();
            var orders = await _context.Orders.Include(o => o.User).Include(o => o.OrderDetails).ThenInclude(od => od.Product).ToListAsync();

            var adminViewModel = new AdminViewModel
            {
                Users = users,
                Orders = orders
            };

            return View(adminViewModel);
        }
    }
}
