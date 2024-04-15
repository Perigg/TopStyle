using TopStyle.Domain.Entities;

namespace TopStyle.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> SearchProductsAsync(string query);
    }
}
