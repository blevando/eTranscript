using eTranscript.Managers;
using eTranscript.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace eTranscript.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentFactoryController : ControllerBase
    {
        private readonly PaymentFactoryManager _payment;
        public PaymentFactoryController(PaymentFactoryManager payment)
        {
            _payment = payment;

        }
        [HttpPost]
        [Route("InitiatePayment")]
        public async Task<Response> InitiatePaymentAsync(string processorType, [FromBody] PaymentRequestDto model)
        {
           var resp = await _payment.InitiatePaymentAsync(processorType, model);
            return resp;

            //var request = HttpContext.Request;
            //var fullUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
            //var fullUrl1 = $"{request.Scheme}://{request.Host}/api/PaymentFactory/InitiatePayment";
        }

        [HttpPost]
        [Route("CyberpayWebhook")]
        [HttpPost]
        public async Task<IActionResult> CyberpayWebHookAsync()
        {
            try
            {
                // Read the request body
                using (var streamReader = new StreamReader(Request.Body))
                {
                    var requestBody = await streamReader.ReadToEndAsync();

                    if (string.IsNullOrEmpty(requestBody))
                    {
                        return BadRequest("Request body is empty.");
                    }

                    // Check if signature exists in headers
                    var headers = Request.Headers;
                    if (!headers.ContainsKey("Cyberpay-Signature"))
                    {
                        return BadRequest("Missing Cyberpay-Signature header.");
                    }

                    // Get the Cyberpay signature
                    var cyberpaySignature = headers["Cyberpay-Signature"].FirstOrDefault();
                    if (string.IsNullOrEmpty(cyberpaySignature))
                    {
                        return BadRequest("Cyberpay-Signature header is empty.");
                    }

                    // Generate hash from the request body using the secret key
                    string key = "XXX";  // Replace with your actual secret key
                    string generatedSignature;

                    using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
                    {
                        var hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestBody));
                        generatedSignature = BitConverter.ToString(hashValue).Replace("-", string.Empty).ToLower(); // Ensure lowercase
                    }

                    // Validate the signature
                    if (!generatedSignature.Equals(cyberpaySignature.ToLower()))
                    {
                        return BadRequest("Invalid Cyberpay signature.");
                    }

                    // If signature is valid, process the payment payload
                    var transactionPayload = JsonConvert.DeserializeObject(requestBody);
                    
                    // TODO: Handle the successful payment (e.g., save transaction, update order status, etc.)

                    return Ok("Webhook processed successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logger here)
                // logger.LogError(ex, "Error processing Cyberpay webhook.");

                return StatusCode(500, "An error occurred while processing the webhook.");
            }
        }


        //public async Task<IActionResult> CyberpayWebHookAsync()
        //{
        //    try
        //    {
        //        // Read the request body
        //        var requestBody = await Request.Content.ReadAsStringAsync();

        //        if (string.IsNullOrEmpty(requestBody))
        //        {
        //            return BadRequest("Request body is empty.");
        //        }

        //        // Check if signature exists in headers
        //        var headers = Request.Headers;
        //        if (!headers.Contains("Cyberpay-Signature"))
        //        {
        //            return BadRequest("Missing Cyberpay-Signature header.");
        //        }

        //        // Get the Cyberpay signature
        //        var cyberpaySignature = headers.GetValues("Cyberpay-Signature").FirstOrDefault();
        //        if (string.IsNullOrEmpty(cyberpaySignature))
        //        {
        //            return BadRequest("Cyberpay-Signature header is empty.");
        //        }

        //        // Generate hash from the request body using the secret key
        //        string key = "XXX";  // Replace with your actual secret key
        //        string generatedSignature;

        //        using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
        //        {
        //            var hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestBody));
        //            generatedSignature = BitConverter.ToString(hashValue).Replace("-", string.Empty).ToLower(); // Ensure lowercase
        //        }

        //        // Validate the signature
        //        if (!generatedSignature.Equals(cyberpaySignature.ToLower()))
        //        {
        //            return BadRequest("Invalid Cyberpay signature.");
        //        }

        //        // If signature is valid, process the payment payload
        //        var transactionPayload = JsonConvert.DeserializeObject(requestBody);

        //        // TODO: Handle the successful payment (e.g., save transaction, update order status, etc.)

        //        return Ok("Webhook processed successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (you can use a logger here)
        //        // logger.LogError(ex, "Error processing Cyberpay webhook.");

        //        return StatusCode(500, "An error occurred while processing the webhook.");
        //    }
        //}

    }
}
