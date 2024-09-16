using eTranscript.Data;
using eTranscript.Models.DomainModels;
using eTranscript.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTranscript.Services.Repositories
{
    public class PaymentFactoryManagement : IPaymentFactoryManagement
    {
        private readonly ApplicationDbContext _context;
        public PaymentFactoryManagement(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<Response> InitiatePaymentAsync(string processorType, PaymentRequestDto model)
        {

            Response response = new Response();
            var paymentConfig = await _context.PaymentGatewayConfig.FirstOrDefaultAsync(p => p.PaymentGatewayId == processorType);

            if (paymentConfig != null)
            {
                model.MerchantRef = paymentConfig.ReferencePrefix + model.CustomerId; // To be changed
                model.ReturnUrl = paymentConfig.PaymentHookUrl;
                model.IntegrationKey = paymentConfig.IntegrationKey;




                IPaymentManagement payment = PaymentFactory.GetPaymentProcessor(processorType);

                var resp = await payment.ProcessPaymentAsync(processorType, model);

                response.Code = 200;
                response.Message = "successful";
                response.Data = resp;
            }
            else
            {
                response.Code = 200;
                response.Message = "successful";
                response.Data = null;
            }

            return await Task.FromResult(response);
        }

    }
}
