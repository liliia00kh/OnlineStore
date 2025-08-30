using Microsoft.AspNetCore.Mvc;
using OnlineStore.DTOs;
using OnlineStore.ExceptionHandling;
using Services.Services;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ProductExceptionFilter))]
    public class ProductController : ControllerBase
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var productDTOs = new List<ProductDTO>();

            foreach (var product in products)
            {
                productDTOs.Add(new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{Path.AltDirectorySeparatorChar}{product.ImageUrl.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)}",
                    CategoryId = product.CategoryId,
                    StockQuantity = product.StockQuantity
                });
            }
            return Ok(productDTOs);
        }
    }
}
