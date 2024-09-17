using eTranscript.Models.DomainModels;
using eTranscript.Services.Interfaces;
using System.Text.Json;
using System.Net.Http;
using System.Text;


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
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://payment-api.staging.cyberpay.ng/api/v1/payments");

            // Set the API key in the headers
            request.Headers.Add("APIkey", "MDc4YjQ4YTVjNjQ0NDJkZGI2M2FjM2QxZjA2MDQxNTM=");

            // Specify the Content-Type as application/json explicitly
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Assign the content to the request
            request.Content = content;

            // Send the request and ensure it is successful
            var result = await client.SendAsync(request);
            result.EnsureSuccessStatusCode();

            // Process the result
            response.Data = await result.Content.ReadAsStringAsync();
            response.Message = $"{processorType}: Using CyberPay";
            response.Code = 200;

            return response;
        }


       
    }
}
