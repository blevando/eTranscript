using eTranscript.Services.Interfaces;

namespace eTranscript.Services.Repositories
{
    public static class PaymentFactory
    {
        public static IPaymentManagement GetPaymentProcessor(string processorType)
        {
            IPaymentManagement processor = null;

            if (processorType.ToLower() == "cyberpay")
            {
                processor = new CyberPayProcessor();
            }
            else if (processorType.ToLower() == "flutterwave")
            {
                processor = new FlutterwaveProcessor();
            }
            else if (processorType.ToLower() == "otherpayment")
            {
                processor = new OtherPaymentProcessor();
            }
            return processor;
        }
    }
}
