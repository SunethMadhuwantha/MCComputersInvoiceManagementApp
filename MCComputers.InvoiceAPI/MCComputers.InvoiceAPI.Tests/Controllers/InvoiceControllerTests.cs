using MCComputers.InvoiceAPI.Controllers;
using MCComputers.InvoiceAPI.Interfaces;
using MCComputers.InvoiceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MCComputers.Tests.Controllers
{
    public class InvoiceControllerTests
    {
        private readonly Mock<IInvoiceService> _mockService;
        private readonly InvoiceController _controller;

        public InvoiceControllerTests()
        {
            _mockService = new Mock<IInvoiceService>();
            _controller = new InvoiceController(_mockService.Object);
        }

        [Fact]
        public async Task CreateInvoice_ReturnsOk_WhenValidInvoice()
        {
            // Arrange
            var invoiceDto = new CreateInvoiceDto
            {
                TransactionDate = DateTime.Now,
                Discount = 5,
                BalanceAmount = 100,
                TotalAmount = 500,
                Items = new List<InvoiceItemDto>
                {
                    new InvoiceItemDto { ProductId = 1, Quantity = 2 }
                }
            };

            _mockService.Setup(x => x.CreateInvoice(invoiceDto)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateInvoice(invoiceDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task CreateInvoice_ReturnsBadRequest_WhenInvoiceIsNull()
        {
            // Act
            var result = await _controller.CreateInvoice(null);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid invoice data.", badRequest.Value);
        }

        [Fact]
        public async Task CreateInvoice_ReturnsBadRequest_WhenItemsListIsEmpty()
        {
            // Arrange
            var invoiceDto = new CreateInvoiceDto
            {
                Discount = 10,
                TotalAmount = 200,
                BalanceAmount = 50,
                Items = new List<InvoiceItemDto>() // Empty list
            };

            // Act
            var result = await _controller.CreateInvoice(invoiceDto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid invoice data.", badRequest.Value);
        }

        [Fact]
        public async Task GetAllInvoices_ReturnsInvoices()
        {
            // Arrange
            var invoiceList = new List<Invoice>
            {
                new Invoice { InvoiceId = 1, TotalAmount = 300 },
                new Invoice { InvoiceId = 2, TotalAmount = 400 }
            };

            _mockService.Setup(s => s.GetAllInvoices()).ReturnsAsync(invoiceList);

            // Act
            var result = await _controller.GetAllInvoices();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Invoice>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.NotNull(okResult.Value);
        }
    }
}
