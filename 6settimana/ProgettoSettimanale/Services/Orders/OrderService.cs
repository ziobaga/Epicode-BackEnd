using ProgettoSettimanale.Context;
using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;

        public OrderService(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _dataContext.Orders.ToListAsync();
            return orders;
        }

        public async Task<Order> IsProcessed(int idOrder)
        {
            var order = _dataContext.Orders.FirstOrDefault(o => o.IdOrder == idOrder);
            if (order.IsProcessed == false)
            {
                order.IsProcessed = true;
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                order.IsProcessed = false;
                await _dataContext.SaveChangesAsync();
            }

            return order;
        }
    }
}
