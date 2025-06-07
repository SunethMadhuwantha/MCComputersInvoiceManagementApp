using MCComputers.InvoiceAPI.Interfaces;
using MCComputers.InvoiceAPI.Models;
using MCComputers.InvoiceAPI.Services;
using Moq;

namespace MCComputers.InvoiceAPI.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockRepository.Object);
        }

        [Fact]
        public async Task AddProductAsync_CallsRepositoryOnce()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Printer" };

            // Act
            await _productService.AddProductAsync(product);

            // Assert
            _mockRepository.Verify(r => r.AddProductAsync(product), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_CallsRepositoryOnce()
        {
            // Arrange
            int id = 5;

            // Act
            await _productService.DeleteProductAsync(id);

            // Assert
            _mockRepository.Verify(r => r.DeleteProductAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsProductList()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Monitor" },
                new Product { ProductId = 2, Name = "Mouse" }
            };

            _mockRepository.Setup(r => r.GetAllProductsAsync()).ReturnsAsync(expectedProducts);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.Equal(expectedProducts, result);
        }

        [Fact]
        public async Task GetProductByIdAsync_ValidId_ReturnsProduct()
        {
            // Arrange
            var product = new Product { ProductId = 3, Name = "Laptop" };

            _mockRepository.Setup(r => r.GetProductByIdAsync(3)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(3);

            // Assert
            Assert.Equal(product, result);
        }

        [Fact]
        public async Task GetProductByIdAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetProductByIdAsync(99)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.GetProductByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateProductAsync_ValidProduct_ReturnsUpdatedProduct()
        {
            // Arrange
            var product = new Product { ProductId = 4, Name = "Tablet" };

            _mockRepository.Setup(r => r.UpdateProductAsync(product)).ReturnsAsync(product);

            // Act
            var result = await _productService.UpdateProductAsync(product);

            // Assert
            Assert.Equal(product, result);
        }
    }
}
