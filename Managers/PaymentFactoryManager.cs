using eTranscript.Models.DomainModels;
using eTranscript.Services.Interfaces;

namespace eTranscript.Managers
{
    public class PaymentFactoryManager
    {
        private readonly IPaymentFactoryManagement _payment;
        public PaymentFactoryManager(IPaymentFactoryManagement payment)
        {
            _payment = payment;
        }


        public async Task<Response> InitiatePaymentAsync(string processorType)
        {
            var resp = await _payment.InitiatePaymentAsync(processorType);
            return resp;
        }
    }
}




 
    
 