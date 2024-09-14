using eTranscript.Models.DomainModels;
using eTranscript.Services.Interfaces;

namespace eTranscript.Services.Repositories
{
    public class PaymentFactoryManagement : IPaymentFactoryManagement
    {
        public async Task<Response> InitiatePaymentAsync(string processorType)
        {

            Response response = new Response();

            IPaymentManagement payment =  PaymentFactory.GetPaymentProcessor(processorType);
            
           var resp = await payment.ProcessPaymentAsync(processorType);

            response.Code = 200;
            response.Message = "successful";
            response.Data = resp;

            return await Task.FromResult(response);
        }
 
    }
}
