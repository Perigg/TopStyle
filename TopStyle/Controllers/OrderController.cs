using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TopStyle.Core.Interfaces;
using TopStyle.Data.Interfaces;
using TopStyle.Domain.DTO;

namespace TopStyle.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;

        public OrderController(IOrderRepository orderRepository, IOrderService orderService)
        {
            _orderRepository = orderRepository;
            _orderService = orderService;
        }



        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            if (orderDto == null || !orderDto.OrderDetails.Any())
            {
                return BadRequest("Invalid order data.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _orderService.CreateOrderAsync(userId, orderDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(new { OrderId = result.OrderId, Message = "Order created successfully." });
        }


        [HttpGet("my-orders")]
        [Authorize]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderRepository.GetOrdersByUserId(userId);
            if (orders.Any())
            {
                return Ok(orders.Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
                    {
                        OrderDetailId = od.OrderDetailId,
                        ProductId = od.ProductId,
                        Quantity = od.Quantity,
                        Price = od.Price,
                        Product = new ProductDto
                        {
                            ProductId = od.Product.ProductId,
                            Name = od.Product.Name,
                            Price = od.Product.Price,
                            Description = od.Product.Description,
                            CategoryId = od.Product.CategoryId

                        }
                    }).ToList()
                }).ToList());
            }
            return NotFound("No orders found for this user.");
        }
    }
}


