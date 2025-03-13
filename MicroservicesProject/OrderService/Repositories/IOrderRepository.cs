using Shared.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId);
        Task<Order> GetOrderWithItemsAsync(int orderId);
        Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);
    }
}