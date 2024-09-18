using eTranscript.Models.DomainModels;

namespace eTranscript.Services.Interfaces
{
    public interface IPaymentManagement
    {               
        Task<Response> ProcessPaymentAsync(string processorType, PaymentRequestDto model);

       
    }
}