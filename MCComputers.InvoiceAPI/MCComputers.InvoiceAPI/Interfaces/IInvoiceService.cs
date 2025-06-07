using MCComputers.InvoiceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCComputers.InvoiceAPI.Interfaces
{
    public interface IInvoiceService
    {
        public Task CreateInvoice(CreateInvoiceDto invoiceDto);

        public Task<ActionResult<IEnumerable<Invoice>>> GetAllInvoices();
    }
}
