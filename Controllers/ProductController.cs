using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using CLDV6211_POE_ST10267411_KhumaloCraft.Models;
using CLDV6211_POE_ST10267411_KhumaloCraft.Data;

namespace CLDV6211_POE_ST10267411_KhumaloCraft.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName) || !IsAdminUser(userName))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName) || !IsAdminUser(userName))
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login");
            }

            var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
            {
                return NotFound();
            }

            var order = _context.Orders.FirstOrDefault(o => o.UserID == int.Parse(userId));

            if (order == null)
            {
                order = new Order
                {
                    UserID = int.Parse(userId),
                    OrderDate = DateTime.Now 

                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            var orderDetail = _context.OrderDetails.FirstOrDefault(od => od.OrderID == order.OrderID && od.ProductID == productId);

            if (orderDetail != null)
            {
                orderDetail.Quantity++;
                orderDetail.TotalPrice = orderDetail.Quantity * product.Price;
            }
            else
            {
                orderDetail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    ProductID = product.ProductID,
                    Quantity = 1,
                    TotalPrice = product.Price
                };
                _context.OrderDetails.Add(orderDetail);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName) || !IsAdminUser(userName))
            {
                return RedirectToAction("Index");
            }

            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        private bool IsAdminUser(string userName)
        {
            return userName == "admin";
        }
    }
}
