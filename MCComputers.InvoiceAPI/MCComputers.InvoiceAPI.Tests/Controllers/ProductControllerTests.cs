using MCComputers.InvoiceAPI.Controllers;
using MCComputers.InvoiceAPI.Interfaces;
using MCComputers.InvoiceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MCComputers.InvoiceAPI.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkWithProducts()
        {
            // Arrange
            var products = new List<Product> { new Product { ProductId = 1, Name = "Laptop" } };
            _mockService.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(products, okResult.Value);
        }

        [Fact]
        public async Task GetProduct_ExistingId_ReturnsProduct()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Monitor" };
            _mockService.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public async Task GetProduct_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetProductByIdAsync(99)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetProduct(99);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Contains("not found", notFound.Value.ToString());
        }

        [Fact]
        public async Task AddProduct_ValidProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var product = new Product { ProductId = 5, Name = "Keyboard" };

            _mockService.Setup(s => s.AddProductAsync(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddProduct(product);

            // Assert
            var createdAt = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetProduct", createdAt.ActionName);
            Assert.Equal(product, createdAt.Value);
        }

        [Fact]
        public async Task UpdateProduct_Valid_ReturnsUpdatedProduct()
        {
            // Arrange
            var product = new Product { ProductId = 2, Name = "Mouse" };

            _mockService.Setup(s => s.UpdateProductAsync(product)).ReturnsAsync(product);

            // Act
            var result = await _controller.UpdateProduct(2, product);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public async Task UpdateProduct_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var product = new Product { ProductId = 3, Name = "Speaker" };

            // Act
            var result = await _controller.UpdateProduct(999, product);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Product ID mismatch.", badRequest.Value);
        }

        [Fact]
        public async Task UpdateProduct_NonExistingProduct_ReturnsNotFound()
        {
            // Arrange
            var product = new Product { ProductId = 10, Name = "Tablet" };

            _mockService.Setup(s => s.UpdateProductAsync(product)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.UpdateProduct(10, product);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var product = new Product { ProductId = 4, Name = "Camera" };

            _mockService.Setup(s => s.GetProductByIdAsync(4)).ReturnsAsync(product);
            _mockService.Setup(s => s.DeleteProductAsync(4)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteProduct(4);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetProductByIdAsync(999)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.DeleteProduct(999);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("not found", notFound.Value.ToString());
        }
    }
}
