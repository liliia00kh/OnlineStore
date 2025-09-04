using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.UnitOfWork;
using Services.Exceptions;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork<OnlineStoreDbContext> _unitOfWork;
        private readonly IProductChangeLogService _productChangeLogService;
        public ProductService(IUnitOfWork<OnlineStoreDbContext> unitOfWork, IProductChangeLogService productChangeLogService)
        {
            _unitOfWork = unitOfWork;
            _productChangeLogService = productChangeLogService;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        public async Task UpdateProductPriceAsync(int productId, decimal newPrice, string changedBy)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product == null) throw new ProductNotFoundException(productId);

            var oldPrice = product.Price;
            product.Price = newPrice;

            await _unitOfWork.SaveAsync();

            await _productChangeLogService.AddLogAsync(new ProductChangeLog
            {
                ProductId = productId,
                ChangedBy = changedBy,
                Field = "Price",
                OldValue = oldPrice.ToString(),
                NewValue = newPrice.ToString(),
                ChangedAt = DateTime.UtcNow
            });
        }
}
}
