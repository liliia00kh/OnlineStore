using DataAccess.Entities;

namespace Services.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task UpdateProductPriceAsync(int productId, decimal newPrice, string changedBy);
    }
}
