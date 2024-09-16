namespace eTranscript.Models.DomainModels
{
    public class PaymentRequestDto
    {

            public string Currency { get; set; }
            public string MerchantRef { get; set; }
            public int Amount { get; set; }
            public string Description { get; set; }
            public string CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string CustomerEmail { get; set; }
            public string CustomerMobile { get; set; }
            public string IntegrationKey { get; set; }
            public string ReturnUrl { get; set; }
            public string ProductCode { get; set; }
            public List<SplitsDto> Splits { get; set; }
        

    }
}
