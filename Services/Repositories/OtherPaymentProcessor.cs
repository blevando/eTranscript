using eTranscript.Models.DomainModels;
using eTranscript.Services.Interfaces;

namespace eTranscript.Services.Repositories
{
    public class OtherPaymentProcessor : IPaymentManagement
    {
        public async Task<Response> ProcessPaymentAsync(string processorType)
        {
            Response response = new Response();
            response.Message = $"{processorType}: Using OtherPayment";
            response.Code = 200;
            response.Data = processorType;

            return await Task.FromResult(response);

        }
    }
}
