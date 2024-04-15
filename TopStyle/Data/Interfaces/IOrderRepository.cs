using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;

namespace TopStyle.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(string userId, IEnumerable<int> productIds);
        Task<List<OrderDto>> GetOrdersByUserId(string userId);
    }
}
