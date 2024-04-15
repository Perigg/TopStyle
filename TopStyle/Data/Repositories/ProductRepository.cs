using Microsoft.EntityFrameworkCore;
using TopStyle.Data.Context;
using TopStyle.Data.Interfaces;
using TopStyle.Domain.Entities;

namespace TopStyle.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TopStyleDbContext _DbContext;

        public ProductRepository(TopStyleDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
        {
            return await _DbContext.Products
                .Where(p => p.Name.Contains(query))
                .ToListAsync();
        }
    }
}
