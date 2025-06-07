using MCComputers.InvoiceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCComputers.InvoiceAPI.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task CreateInvoice(Invoice invoiceDto);

        public Task<ActionResult<IEnumerable<Invoice>>> GetAllInvoices();
    }
}
