using MCComputers.InvoiceAPI.DATA;
using MCComputers.InvoiceAPI.Interfaces;
using MCComputers.InvoiceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCComputers.InvoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto invoice)
        {
            if (invoice == null || invoice.Items == null || !invoice.Items.Any())
                return BadRequest("Invalid invoice data.");

            try
            {
                await _invoiceService.CreateInvoice(invoice);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(new { message = "Invoice saved successfully." });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoices();
            return Ok(invoices);
        }
    }
}
