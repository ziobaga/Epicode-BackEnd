using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale.Models;
using ProgettoSettimanale.Models.ViewModels;
using ProgettoSettimanale.Services.Products;

namespace ProgettoSettimanale.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("Product/CreateProducts")]
        public async Task<IActionResult> CreateProducts()
        {
            var ingredients = await _productService.GetAllIngredientsAsync();
            var viewModel = new ProductCreateVM
            {
                Product = new Product
                {
                    Name = "",
                    Price = 0.0m,
                    DeliveryTimeMin = 0
                },
                Ingredients = ingredients
            };
            return View(viewModel);
        }

        [HttpPost("Product/CreateProducts")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductCreateVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.CreateProductIngredientsAsync(viewModel);
                return RedirectToAction("ListProducts");
            }
            return View(viewModel);
        }

        [HttpGet("Product/ListProducts")]
        public async Task<IActionResult> ListProducts()
        {
            var products = await _productService.GetAllProductsIngredientsAsync();
            return View(products);
        }

        [HttpPost("Product/DeleteProduct/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
            {
                
                return NotFound();
            }

            return RedirectToAction("ListProducts");
        }
    }
}
