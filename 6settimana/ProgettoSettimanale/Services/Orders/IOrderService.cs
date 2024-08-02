using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Services.Orders
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> IsProcessed(int idOrder);
        
    }
}
