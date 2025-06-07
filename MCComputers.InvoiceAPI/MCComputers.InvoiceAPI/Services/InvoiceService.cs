using MCComputers.InvoiceAPI.Interfaces;
using MCComputers.InvoiceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCComputers.InvoiceAPI.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository, IProductRepository productRepository)
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
        }

        public async Task CreateInvoice(CreateInvoiceDto invoiceDto)
        {
            List<Product> products = new();
            List<InvoiceProduct> invoiceProducts = new();

            foreach (var item in invoiceDto.Items)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception("Invalid Product ID");
                }
                products.Add(product);
                invoiceProducts.Add(new InvoiceProduct
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            var invoice = new Invoice
            {
                InvoiceDate = invoiceDto.TransactionDate,
                TotalAmount = invoiceDto.TotalAmount,
                BalanceAmount = invoiceDto.BalanceAmount,
                Discount = invoiceDto.Discount,
                InvoiceProducts = invoiceProducts
            };

            await _invoiceRepository.CreateInvoice(invoice);
        }

        public Task<ActionResult<IEnumerable<Invoice>>> GetAllInvoices()
        {
            return _invoiceRepository.GetAllInvoices();
        }
    }
}
