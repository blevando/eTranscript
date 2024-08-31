using eTranscript.Models.DomainModels;
using eTranscript.Models.EntityModels;
using eTranscript.Services.Interfaces;

namespace eTranscript.Managers
{
    public class OrderManager
    {
        private readonly IOrderManagement _order;
        public OrderManager(IOrderManagement order)
        {
            _order = order;
        }

        public async Task<Response> CreateOrderAsync(string customerId, Commodity model)
        {
            var resp = await _order.CreateOrderAsync(customerId,   model);
        return resp;
        }


    }
}
