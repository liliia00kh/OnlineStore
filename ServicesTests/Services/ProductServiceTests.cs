using DataAccess.Context;
using DataAccess.Entities;
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
        public async void GetAllProductsAsync_GetAll_ReturnEmptyList()
        {
            _unitOfWorkMock.Setup(x => x.ProductRepository).Returns(_productRepoMock.Object);
            IProductService productService = new ProductService(_unitOfWorkMock.Object);
            var result = await productService.GetAllProductsAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async void GetAllProductsAsync_GetAll_ReturnElements()
        {
            _productRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync([new Product() { Id = 1, Name = "Bag" }, new Product() { Id = 2, Name = "Bag" }]);
            _unitOfWorkMock.Setup(x => x.ProductRepository).Returns(_productRepoMock.Object);
            IProductService productService = new ProductService(_unitOfWorkMock.Object);
            var result = await productService.GetAllProductsAsync();

            Assert.Equal(2, result.Count());
        }
    }
}
