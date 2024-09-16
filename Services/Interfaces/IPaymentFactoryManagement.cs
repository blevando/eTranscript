using eTranscript.Models.DomainModels;

namespace eTranscript.Services.Interfaces
{
    public interface IPaymentFactoryManagement
    {

        Task<Response> InitiatePaymentAsync(string processorType, PaymentRequestDto model);
    }
}
