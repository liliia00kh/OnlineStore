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
        IProductChangeLogService _productChangeLogService;
        public ProductController(IProductService productService, IProductChangeLogService productChangeLogService)
        {
            _productService = productService;
            _productChangeLogService = productChangeLogService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
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

        [HttpPut("update-price")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProductPrice([FromBody] UpdateProductPriceDTO dto)
        {
            if (dto.NewPrice <= 0)
                return BadRequest("Price must be greater than zero");

            var username = User.Identity?.Name ?? "system";

            await _productService.UpdateProductPriceAsync(dto.ProductId, dto.NewPrice, username);

            return Ok(new { message = "Product price updated successfully" });
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetProductHistory(int id)
        {
            var logs = await _productChangeLogService.GetLogsAsync(id);
            return Ok(logs);
        }

        [HttpGet("cancelable-task")]
        public async Task<IActionResult> GetProductsAsync(CancellationToken cancellationToken)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await Task.Delay(1000, cancellationToken); // асинхронна затримка з підтримкою токена
                }

                return Ok(new[] { "Product1", "Product2", "Product3" });
            }
            catch (OperationCanceledException)
            {
                return BadRequest("Запит скасовано клієнтом");
            }
        }

    }
}
