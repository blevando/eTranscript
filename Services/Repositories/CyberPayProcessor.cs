using eTranscript.Models.DomainModels;
using eTranscript.Services.Interfaces;
using System.Text.Json;
using System.Net.Http;


namespace eTranscript.Services.Repositories
{
    public class CyberPayProcessor : IPaymentManagement
    {
         

        public CyberPayProcessor()
        {
        }
 

        public async Task<Response> ProcessPaymentAsync(string processorType, PaymentRequestDto model)
        {
            Response response = new Response();



         

            var jsonString = JsonSerializer.Serialize(model);

           // var  = new StringContent(jsonString);
            var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, "https://payment-api.staging.cyberpay.ng/api/v1/payments");

                request.Headers.Add("APIkey", "MDc4YjQ4YTVjNjQ0NDJkZGI2M2FjM2QxZjA2MDQxNTM=");
                // var content = new StringContent("{\r\n    \"Currency\": \"NGN\",\r\n    \"MerchantRef\": \"9552354668\",\r\n    \"Amount\": 50000,\r\n    \"Description\": \"showmax subscription\",\r\n    \"CustomerId\": \"191\",\r\n    \"CustomerName\": \"Josie\",\r\n    \"CustomerEmail\": \"Flavie_Turcotte@yahoo.com\",\r\n    \"CustomerMobile\": \"401-954-6342\",\r\n    \"IntegrationKey\": \"078b48a5c64442ddb63ac3d1f0604153\",\r\n    \"ReturnUrl\": \"http://www.*******.com\",\r\n    \"WebhookUrl\": \"https://merchant_webhook_url\",\r\n    \"ProductCode\": \"CAR_LOAN\",\r\n    \"Splits\": [\r\n        {\r\n            \"WalletCode\": \"teargstd\",\r\n            \"Amount\": 50000,\r\n            \"ShouldDeductFrom\": true\r\n        }\r\n    ]\r\n}", null, "application/json");
                var content = new StringContent(jsonString);
                request.Content = content;
                var result = await client.SendAsync(request);
                result.EnsureSuccessStatusCode();

                response.Data = result.Content.ReadAsStringAsync();
                response.Message = $"{processorType}: Using CyberPay";
                response.Code = 200;
            
            return await Task.FromResult(response);

        }
    }
}
