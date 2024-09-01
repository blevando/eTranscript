namespace eTranscript.Models.EntityModels
{
    public class Invoice
    {
        public int Id { get; set; }  
        public string InvoiceNumber { get; set; }
        public string? OrderNumber { get; set; }
        public decimal DocumentSubTotal { get; set; }
        public decimal ShippmentSubtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }        
        public int Status { get; set; } // Paid or not paid


    }
}
