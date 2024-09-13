using System.ComponentModel.DataAnnotations.Schema;

namespace eTranscript.Models.DomainModels
{
    public class InvoiceDto
    {
        public string InvoiceNumber { get; set; }
        public string? OrderNumber { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal DocumentSubTotal { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ShippmentSubtotal { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalAmount { get; set; }
        public int Status { get; set; } // Paid or not paid

    }
}
