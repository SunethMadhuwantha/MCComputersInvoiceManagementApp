using MCComputers.InvoiceAPI.Interfaces;
using MCComputers.InvoiceAPI.Models;
using MCComputers.InvoiceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace MCComputers.InvoiceAPI.Tests.Services
{
    public class InvoiceServiceTests
    {
        private readonly Mock<IInvoiceRepository> _mockInvoiceRepo;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly InvoiceService _invoiceService;

        public InvoiceServiceTests()
        {
            _mockInvoiceRepo = new Mock<IInvoiceRepository>();
            _mockProductRepo = new Mock<IProductRepository>();
            _invoiceService = new InvoiceService(_mockInvoiceRepo.Object, _mockProductRepo.Object);
        }

        [Fact]
        public async Task CreateInvoice_ValidItems_SuccessfullyCreatesInvoice()
        {
            // Arrange
            var invoiceDto = new CreateInvoiceDto
            {
                TransactionDate = DateTime.Now,
                TotalAmount = 300,
                BalanceAmount = 50,
                Discount = 20,
                Items = new List<InvoiceItemDto>
                {
                    new InvoiceItemDto { ProductId = 1, Quantity = 2 },
                    new InvoiceItemDto { ProductId = 2, Quantity = 1 }
                }
            };

            var product1 = new Product { ProductId = 1, Price = 100 };
            var product2 = new Product { ProductId = 2, Price = 100 };

            _mockProductRepo.Setup(p => p.GetProductByIdAsync(1)).ReturnsAsync(product1);
            _mockProductRepo.Setup(p => p.GetProductByIdAsync(2)).ReturnsAsync(product2);

            _mockInvoiceRepo.Setup(i => i.CreateInvoice(It.IsAny<Invoice>())).Returns(Task.CompletedTask);

            // Act
            await _invoiceService.CreateInvoice(invoiceDto);

            // Assert
            _mockInvoiceRepo.Verify(i => i.CreateInvoice(It.Is<Invoice>(inv =>
                inv.InvoiceProducts.Count == 2 &&
                inv.TotalAmount == invoiceDto.TotalAmount &&
                inv.BalanceAmount == invoiceDto.BalanceAmount &&
                inv.Discount == invoiceDto.Discount
            )), Times.Once);
        }

        [Fact]
        public async Task CreateInvoice_SomeProductsNotFound_SkipsMissingProducts()
        {
            // Arrange
            var invoiceDto = new CreateInvoiceDto
            {
                TransactionDate = DateTime.Now,
                TotalAmount = 200,
                BalanceAmount = 0,
                Discount = 0,
                Items = new List<InvoiceItemDto>
                {
                    new InvoiceItemDto { ProductId = 1, Quantity = 1 },
                    new InvoiceItemDto { ProductId = 999, Quantity = 1 } // Not found
                }
            };

            var product1 = new Product { ProductId = 1, Price = 200 };

            _mockProductRepo.Setup(p => p.GetProductByIdAsync(1)).ReturnsAsync(product1);
            _mockProductRepo.Setup(p => p.GetProductByIdAsync(999)).ReturnsAsync((Product)null);

            _mockInvoiceRepo.Setup(i => i.CreateInvoice(It.IsAny<Invoice>())).Returns(Task.CompletedTask);

            // Act
            await _invoiceService.CreateInvoice(invoiceDto);

            // Assert
            _mockInvoiceRepo.Verify(i => i.CreateInvoice(It.Is<Invoice>(inv =>
                inv.InvoiceProducts.Count == 1 &&
                inv.InvoiceProducts.FirstOrDefault().ProductId == 1
            )), Times.Once);
        }

        [Fact]
        public async Task GetAllInvoices_ReturnsInvoices()
        {
            // Arrange
            var invoices = new List<Invoice>
            {
                new Invoice { InvoiceId = 1, TotalAmount = 100 },
                new Invoice { InvoiceId = 2, TotalAmount = 200 }
            };

            _mockInvoiceRepo.Setup(r => r.GetAllInvoices())
                .ReturnsAsync(new ActionResult<IEnumerable<Invoice>>(invoices));

            // Act
            var result = await _invoiceService.GetAllInvoices();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Invoice>>>(result);
            var invoiceList = Assert.IsAssignableFrom<IEnumerable<Invoice>>(okResult.Value);
            Assert.Equal(2, ((List<Invoice>)invoiceList).Count);
        }
    }
}
