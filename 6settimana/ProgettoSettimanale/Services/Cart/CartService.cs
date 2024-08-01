using ProgettoSettimanale.Context;
using ProgettoSettimanale.Models;
using ProgettoSettimanale.Models.ViewModels;
using ProgettoSettimanale.Services.Products;

namespace ProgettoSettimanale.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly IProductService _productService;
        private static List<CartVM> _temporaryCart = new List<CartVM>();

        private readonly DataContext _dataContext;

        public CartService(IProductService productService, DataContext dataContext)
        {
            _productService = productService;
            _dataContext = dataContext;
        }

        public async Task AddToCartAsync(int productId, int quantity)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product != null)
            {
                var existingCartItem = _temporaryCart.FirstOrDefault(c => c.ProductId == productId);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    var newCartItem = new CartVM
                    {
                        ProductId = product.IdProduct,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = quantity,
                    };
                    _temporaryCart.Add(newCartItem);
                }
            }
        }


        public async Task AddMultipleToCartAsync(Dictionary<int, int> products)
        {
            foreach (var product in products)
            {
                await AddToCartAsync(product.Key, product.Value);
            }
        }

        public Task<List<CartVM>> GetCartItemsAsync()
        {
            return Task.FromResult(_temporaryCart);
        }

        public Task RemoveFromCartAsync(int productId)
        {
            var itemToRemove = _temporaryCart.FirstOrDefault(c => c.ProductId == productId);
            if (itemToRemove != null)
            {
                _temporaryCart.Remove(itemToRemove);
            }
            return Task.CompletedTask;
        }

        public Task<decimal> GetTotalAmountAsync()
        {
            var totalAmount = _temporaryCart.Sum(c => c.Price * c.Quantity);
            return Task.FromResult(totalAmount);
        }

        public Task<List<CartVM>> ClearCartAsync()
        {
            _temporaryCart.Clear();
            return Task.FromResult(_temporaryCart);
        }

        public async Task CreateOrderAsync(Order o)
        {
            var cartItems = await GetCartItemsAsync();

           



            var order = new Order
            {
                Address = o.Address,
                AdditionalNotes = o.AdditionalNotes,
                IsProcessed = false,
                OrderDate = DateTime.Now,
                TotalAmount = await GetTotalAmountAsync(),
                IdUser = o.IdUser,
            };

            _dataContext.Orders.Add(order);
            await _dataContext.SaveChangesAsync();

            foreach (var cartItem in cartItems)
            {
                var orderedProduct = new OrderedProduct
                {
                    IdOrder = order.IdOrder,
                    IdProduct = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                };

                _dataContext.OrderedProducts.Add(orderedProduct);
            }

            await _dataContext.SaveChangesAsync();

            await ClearCartAsync();
        }

    }
}
