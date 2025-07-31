using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.UnitOfWork;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork<OnlineStoreDbContext> _unitOfWork;
        public ProductService(IUnitOfWork<OnlineStoreDbContext> unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }
    }
}
