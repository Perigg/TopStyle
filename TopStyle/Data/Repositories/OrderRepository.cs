using Microsoft.EntityFrameworkCore;
using TopStyle.Data.Context;
using TopStyle.Data.Interfaces;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;

namespace TopStyle.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TopStyleDbContext _dbContext;

        public OrderRepository(TopStyleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> CreateOrderAsync(string userId, IEnumerable<int> productIds)
        {
            var order = new Order { UserId = userId };
            _dbContext.Orders.Add(order);

            foreach (var productId in productIds)
            {
                var orderDetail = new OrderDetail { OrderId = order.OrderId, ProductId = productId, Quantity = 1 };
                _dbContext.OrderDetails.Add(orderDetail);

            }

            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<List<OrderDto>> GetOrdersByUserId(string userId)
        {
            var orders = await _dbContext.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
                    {
                        OrderDetailId = od.OrderDetailId,
                        ProductId = od.ProductId ?? 0,
                        Quantity = od.Quantity,
                        Price = od.Price,
                        Product = new ProductDto
                        {
                            ProductId = od.Product.ProductId,
                            Name = od.Product.Name,
                            Price = od.Product.Price,
                            Description = od.Product.Description,
                            CategoryId = od.Product.CategoryId // Detta antar att Product inkluderar CategoryId
                        }
                    }).ToList()
                }).ToListAsync();
            return orders;
        }
    }
}

