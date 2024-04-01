
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CLDV6211_POE_ST10267411_KhumaloCraft.Controllers
{
    public class ProductsController : Controller
    {
       
        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
