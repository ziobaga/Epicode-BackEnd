using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale.Context;
using ProgettoSettimanale.Services.Orders;
using Microsoft.EntityFrameworkCore;

namespace ProgettoSettimanale.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly DataContext _dataContext;
        public OrderController(IOrderService orderService, DataContext dataContext)
        {
            _orderService = orderService;
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> ListOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsProcessed(int idOrder)
        {
            await _orderService.IsProcessed(idOrder);
            return RedirectToAction("ListOrders");
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessedOrdersCount()
        {
            var processedOrdersCount = await _dataContext.Orders.CountAsync(o => o.IsProcessed == true);

            return Ok(processedOrdersCount);
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalIncome()
        {
            var totalIncome = await _dataContext.Orders
                .Where(o => o.IsProcessed == true) 
                .SumAsync(o => o.TotalAmount);
            return Ok(totalIncome);
        }
    }
}
