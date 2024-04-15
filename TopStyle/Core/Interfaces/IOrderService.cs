using TopStyle.Domain.DTO;

namespace TopStyle.Core.Interfaces
{
    public interface IOrderService
    {
        Task<(bool Success, string Message, int OrderId)> CreateOrderAsync(string userId, CreateOrderDto orderDto);
    }
}

