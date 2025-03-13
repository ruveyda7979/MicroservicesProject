using OrderService.Services;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ProductHttpClient _productClient;
        private readonly UserHttpClient _userClient;

        public OrdersController(
            IOrderRepository orderRepository, 
            ProductHttpClient productClient,
            UserHttpClient userClient)
        {
            _orderRepository = orderRepository;
            _productClient = productClient;
            _userClient = userClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _orderRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUser(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}/items")]
        public async Task<ActionResult<Order>> GetOrderWithItems(int id)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var createdOrder = await _orderRepository.AddAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.OrderId }, createdOrder);
        }

        [HttpPost("items")]
        public async Task<ActionResult<OrderItem>> AddOrderItem(OrderItem orderItem)
        {
            var createdItem = await _orderRepository.AddOrderItemAsync(orderItem);
            return CreatedAtAction(nameof(GetOrder), new { id = createdItem.OrderId }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.OrderId)
                return BadRequest();

            await _orderRepository.UpdateAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}