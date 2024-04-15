using TopStyle.Core.Interfaces;
using TopStyle.Data.Context;

namespace TopStyle.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly TopStyleDbContext _dbContext;

        public ProductService(TopStyleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public decimal GetPriceById(int productId)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null) throw new ArgumentException("Product not found");
            return product.Price;
        }
    }
}
