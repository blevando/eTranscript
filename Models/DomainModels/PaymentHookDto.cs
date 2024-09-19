namespace eTranscript.Models.DomainModels
{
    public class PaymentHookDto
    {

         
            public string status { get; set; }
            public string message { get; set; }
            public string reference { get; set; }
            public int amount { get; set; }
            public string merchantRef { get; set; }
            public string transactionDate { get; set; }
            public string customerId { get; set; }
            public string customerName { get; set; }
            public string accountNumber { get; set; }
        
    }
}
