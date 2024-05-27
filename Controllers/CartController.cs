using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CLDV6211_POE_ST10267411_KhumaloCraft.Models;
using CLDV6211_POE_ST10267411_KhumaloCraft.Data;

namespace CLDV6211_POE_ST10267411_KhumaloCraft.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login");
            }

            var order = await _context.Orders
                .Where(o => o.UserID == int.Parse(userId))
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync();

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(int orderDetailId)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(orderDetailId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
