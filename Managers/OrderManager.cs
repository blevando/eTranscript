using eTranscript.Models.DomainModels;
using eTranscript.Models.EntityModels;
using eTranscript.Services.Interfaces;
using System.Collections.Generic;

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
            var resp = await _order.CreateOrderAsync(customerId, model);
            return resp;
        }

        public async Task<Response> AddShipmentToOrderDetailAsync(string OrderNumber, List<ShipmentDto> model)
        {
            var resp = await _order.AddShipmentToOrderDetailAsync(OrderNumber,  model);
            return resp;

        }
        //
        public async Task<Response> CreateInvoiceAsync(string OrderNumber)
        {
            var resp = await _order.CreateInvoiceAsync(OrderNumber);
            return resp;
        }





        }
}
