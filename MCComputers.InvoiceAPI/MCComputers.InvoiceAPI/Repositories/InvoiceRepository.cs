using MCComputers.InvoiceAPI.DATA;
using MCComputers.InvoiceAPI.Interfaces;
using MCComputers.InvoiceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCComputers.InvoiceAPI.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Invoice>>> GetAllInvoices()
        {
            return await  _context.Invoices
                .Include(i => i.InvoiceProducts)
                .ThenInclude(ip => ip.Product)
                .ToListAsync();
        }
    }
}
