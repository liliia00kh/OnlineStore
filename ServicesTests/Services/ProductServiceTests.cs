using DataAccess.Context;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using Moq;
using Services.Services;

namespace ServicesTests.Services
{
    public class ProductServiceTests
    {
        Mock<IUnitOfWork<OnlineStoreDbContext>> _unitOfWorkMock;
        private readonly Mock<IProductRepository> _productRepoMock;
        public ProductServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork<OnlineStoreDbContext>>();
            _productRepoMock = new Mock<IProductRepository>();
        }
        [Fact]
        public async void GetAllProductsAsync_GetAll_ReturnAll()
        {
            _unitOfWorkMock.Setup(x => x.ProductRepository).Returns(_productRepoMock.Object);
            IProductService productServiceTests = new ProductService(_unitOfWorkMock.Object);
            var result = await productServiceTests.GetAllProductsAsync();

            Assert.Empty(result);
        }
    }
}
