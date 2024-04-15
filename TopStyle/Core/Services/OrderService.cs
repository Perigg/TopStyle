using TopStyle.Core.Interfaces;
using TopStyle.Data.Context;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;

namespace TopStyle.Core.Services
{
    public class OrderService : IOrderService

    {
        private readonly TopStyleDbContext _dbContext;
        private readonly IProductService _productService;

        public OrderService(TopStyleDbContext dbContext, IProductService productService)
        {
            _dbContext = dbContext;
            _productService = productService;
        }

        public async Task<(bool Success, string Message, int OrderId)> CreateOrderAsync(string userId, CreateOrderDto orderDto)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDetails = orderDto.OrderDetails.Select(od => new OrderDetail
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = _productService.GetPriceById(od.ProductId)
                }).ToList()
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return (true, "Order created successfully.", order.OrderId);
        }

    }
}
