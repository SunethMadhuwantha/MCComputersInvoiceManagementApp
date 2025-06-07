using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MCComputers.InvoiceAPI.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;


        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal Discount { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal BalanceAmount { get; set; }

        public ICollection<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();
    }
}
