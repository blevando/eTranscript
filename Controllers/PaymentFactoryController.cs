using eTranscript.Managers;
using eTranscript.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

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
        [HttpGet]
        [Route("InitiatePayment")]
        public async Task<Response> InitiatePaymentAsync(string processorType)
        {
            var resp = await _payment.InitiatePaymentAsync(processorType);
            return resp;
        }
    }
}
