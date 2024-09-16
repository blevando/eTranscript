namespace eTranscript.Models.EntityModels
{
    public class Order
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; } // Unique string
        public  string OrderDate { get; set; }
        public string CustomerId { get; set; } // matric number        
        public string? PaymentGateway { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentReference { get; set; }
        public int Status { get; set; }
        public string? Note { get; set; }

    }
}
