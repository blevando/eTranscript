using System.ComponentModel.DataAnnotations.Schema;

namespace eTranscript.Models.EntityModels
{
    public class Receipt
    {
        public int Id { get; set; }
        public string ReceiptNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string OrderNumber { get; set; }
       
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalAmount { get; set; }
        public string PaymentDate { get; set; }
        public bool Status { get; set; }
       
    }
}
