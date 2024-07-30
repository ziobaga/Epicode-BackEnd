using Microsoft.AspNetCore.Mvc;

namespace ProgettoSettimanale.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
