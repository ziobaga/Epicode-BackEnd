using ProgettoSettimanale.Models;
using ProgettoSettimanale.Models.ViewModels;

namespace ProgettoSettimanale.Services.Cart
{
    public interface ICartService
    {
        Task AddToCartAsync(int productId, int quantity);
        Task<List<CartVM>> GetCartItemsAsync();
        Task RemoveFromCartAsync(int productId);
        Task<decimal> GetTotalAmountAsync();
        Task<List<CartVM>> ClearCartAsync();
        Task AddMultipleToCartAsync(Dictionary<int, int> products);

        Task CreateOrderAsync(Order o);
    }
}
