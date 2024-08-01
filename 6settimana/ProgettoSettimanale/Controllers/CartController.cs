using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale.Context;
using ProgettoSettimanale.Models;
using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale.Services.Cart;

namespace ProgettoSettimanale.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly DataContext _dataContext;


        public CartController(ICartService cartService, DataContext dataContext)
        {
            _cartService = cartService;
            _dataContext = dataContext;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> AddMultipleToCart(Dictionary<int, int> products)
        {
            if (products == null || !products.Any())
            {
                return BadRequest("Nessun prodotto selezionato.");
            }

            await _cartService.AddMultipleToCartAsync(products);
            return RedirectToAction("ListCart", "Cart");
        }

        [HttpGet]
        public async Task<IActionResult> ListCart()
        {
            var cartItems = await _cartService.GetCartItemsAsync();
            var totalAmount = await _cartService.GetTotalAmountAsync();
            ViewBag.TotalAmount = totalAmount;
            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await _cartService.RemoveFromCartAsync(productId);
            return RedirectToAction("ListCart");
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            var cartItems = await _cartService.GetCartItemsAsync();
            var totalAmount = await _cartService.GetTotalAmountAsync();
            ViewBag.TotalAmount = totalAmount;
            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetOrder(string address, string additionalNotes)
        {
            var userName = User.Identity!.Name;
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == userName);
            try
            {
                var order = new Order
                {
                    Address = address,
                    AdditionalNotes = additionalNotes,
                    IdUser = user!.IdUser
                };

                await _cartService.CreateOrderAsync(order);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return View(await _cartService.GetCartItemsAsync());
            }
        }
    }
}
