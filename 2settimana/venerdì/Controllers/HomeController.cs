using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using venerdì.Entities;
using venerdì.Models;
using venerdì.Service;

namespace venerdì.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly iArticleService _articleService;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, iArticleService articleService, IWebHostEnvironment env)
        {
            _logger = logger;
            _articleService = articleService;
            _env = env;
        }

        public IActionResult Index()
        {
            var articles = _articleService.GetAll().OrderByDescending(a => a.PublishedAt);
            return View(articles);
        }

        public IActionResult Write()
        {
            return View(new Article());
        }

        [HttpPost]
        public async Task<IActionResult> Write(Article article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }

            var a = new Article
            {
                Name = article.Name,
                Price = article.Price,
                PublishedAt = DateTime.Now,
                Description = article.Description
            };

            _articleService.Create(a);

            string uploads = Path.Combine(_env.WebRootPath, "images");

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            if (article.CoverImage != null)
            {
                var coverImgPath = Path.Combine(uploads, $"{a.Id}_cover.jpg");
                using (var fileStream = new FileStream(coverImgPath, FileMode.Create))
                {
                    await article.CoverImage.CopyToAsync(fileStream);
                }
            }

            if (article.AdditionalImage1 != null)
            {
                var img1Path = Path.Combine(uploads, $"{a.Id}_img1.jpg");
                using (var fileStream = new FileStream(img1Path, FileMode.Create))
                {
                    await article.AdditionalImage1.CopyToAsync(fileStream);
                }
            }

            if (article.AdditionalImage2 != null)
            {
                var img2Path = Path.Combine(uploads, $"{a.Id}_img2.jpg");
                using (var fileStream = new FileStream(img2Path, FileMode.Create))
                {
                    await article.AdditionalImage2.CopyToAsync(fileStream);
                }
            }

            return RedirectToAction("ReadById", new { id = a.Id });
        }


        public IActionResult ReadById(int id)
        {
            var article = _articleService.GetById(id);
            if (article == null)
            {
                return NotFound();
            }

            string uploads = Path.Combine(_env.WebRootPath, "images");
            ViewBag.coverImg = System.IO.File.Exists(Path.Combine(uploads, $"{id}_cover.jpg")) ? $"/images/{id}_cover.jpg" : null;
            ViewBag.img1 = System.IO.File.Exists(Path.Combine(uploads, $"{id}_img1.jpg")) ? $"/images/{id}_img1.jpg" : null;
            ViewBag.img2 = System.IO.File.Exists(Path.Combine(uploads, $"{id}_img2.jpg")) ? $"/images/{id}_img2.jpg" : null;

            return View(article);
        }

        public IActionResult Articles()
        {
            var articles = _articleService.GetAll().OrderByDescending(a => a.PublishedAt);
            return View(articles);
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
