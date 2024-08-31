namespace eTranscript.Models.EntityModels
{
    public class Invoice
    {
        public int Id { get; set; }  
        public string InvoiceNumber { get; set; }
        public string? OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }        
        public int Status { get; set; } // Paid or not paid


    }
}
