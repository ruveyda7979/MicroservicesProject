using Microsoft.EntityFrameworkCore;
using Shared.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly OrderDbContext _orderContext;

        public OrderRepository(OrderDbContext context) : base(context)
        {
            _orderContext = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId)
        {
            return await _orderContext.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order> GetOrderWithItemsAsync(int orderId)
        {
            return await _orderContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
        {
            await _orderContext.OrderItems.AddAsync(orderItem);
            await _orderContext.SaveChangesAsync();
            return orderItem;
        }
    }
}