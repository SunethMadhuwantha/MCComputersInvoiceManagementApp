using System.ComponentModel.DataAnnotations;

public class CreateInvoiceDto
{
    [Required]
    public DateTime TransactionDate { get; set; } = DateTime.Now;

    [Required]
    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal Discount { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal BalanceAmount { get; set; }

    public List<InvoiceItemDto> Items { get; set; } = new();
}

// Fluent ValidAtion
