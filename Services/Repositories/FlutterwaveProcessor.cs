﻿using eTranscript.Models.DomainModels;
using eTranscript.Services.Interfaces;

namespace eTranscript.Services.Repositories
{
    public class FlutterwaveProcessor : IPaymentManagement
    {
        public FlutterwaveProcessor()
        {
            
        }

        public async Task<Response> ProcessPaymentAsync(string processorType, PaymentRequestDto model)
        {
            Response response = new Response();
            response.Message = $"{processorType}: Using Flutterwave";
            response.Code = 200;
            response.Data = processorType;

            return await Task.FromResult(response);

        }
    }
}
