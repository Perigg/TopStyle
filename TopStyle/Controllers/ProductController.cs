using Microsoft.AspNetCore.Mvc;
using TopStyle.Data.Interfaces;

namespace TopStyle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;

        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet("search")]

        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                _logger.LogWarning("Search query is empty.");
                return BadRequest("Query parameter is required");
            }

            try
            {
                var products = await _productRepository.SearchProductsAsync(query);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products with query {Query}", query);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
