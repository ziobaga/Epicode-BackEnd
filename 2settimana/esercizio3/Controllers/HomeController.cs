using esercizio3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace esercizio3.Controllers
{
    public class HomeController : Controller
    {
        private static List<Sala> sale = new List<Sala>
{
        new Sala { Nome = "SALA NORD" },
        new Sala { Nome = "SALA EST" },
        new Sala { Nome = "SALA SUD" }
};


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
