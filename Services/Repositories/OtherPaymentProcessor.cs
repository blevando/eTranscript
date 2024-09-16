using eTranscript.Models.DomainModels;
using eTranscript.Models.EntityModels;
using eTranscript.Services.Interfaces;

namespace eTranscript.Services.Repositories
{
    public class OtherPaymentProcessor : IPaymentManagement
    {

        public OtherPaymentProcessor()
        {
            
        }
        public async Task<Response> ProcessPaymentAsync(string processorType, PaymentRequestDto model)
        {
            Response response = new Response();
            response.Message = $"{processorType}: Using OtherPayment";
            response.Code = 200;
            response.Data = processorType;

            return await Task.FromResult(response);

        }
    }
}
