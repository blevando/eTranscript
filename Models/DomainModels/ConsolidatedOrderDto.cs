using eTranscript.Models.EntityModels;

namespace eTranscript.Models.DomainModels
{
    public class ConsolidatedOrderDto
    {
        public string? OrderNumber { get; set; }
        public string CustomerId { get; set; } 
        public string CustomerName { get; set; }

        public string? PaymentGateway { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentReference { get; set; }

        public DocumentDto DocumentDto { get; set; }

              
        public List<ShipmentDto> ShipmentDto { get; set; }

        public InvoiceDto InvoiceDto { get; set; }

    }
}
