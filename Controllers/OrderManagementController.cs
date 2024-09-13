using eTranscript.Managers;
using eTranscript.Models.DomainModels;
using eTranscript.Models.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eTranscript.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagementController : ControllerBase
    {
        private readonly OrderManager _orderManager;
        public OrderManagementController(OrderManager orderManager)
        {
            _orderManager = orderManager;

        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<Response> CreateOrderAsync(string customerId, [FromBody] CommodityDto model)
        {
           
            var res = await _orderManager.CreateOrderAsync(customerId, model );

            return res;
        }

        [HttpGet]
        [Route("GetOrderByNumber")]
        public async Task<Response> GetOrderByNumberAsync(string OrderNumber)
        {
            var res = await _orderManager.GetOrderByNumberAsync(OrderNumber);


            return res;
        }


        [HttpPost]
        [Route("AddShipmentToOrderDetails")]
        public async Task<Response> AddShipmentToOrderDetailAsync(string OrderNumber, [FromBody] List<ShipmentDto> model)
        {
            
                var res = await _orderManager.AddShipmentToOrderDetailAsync(OrderNumber, model);

                return res;
 
             
        }


        [HttpPost]
        [Route("AddInvoice")]
        public async Task<Response> AddInvoiceAsync(string OrderNumber)
        {

            var res = await _orderManager.CreateInvoiceAsync(OrderNumber);

            return res;


        }


        [HttpPost]
        [Route("AddCommodityToOrderDetail")]

        public async Task<Response> AddCommodityToOrderDetailAsync(string OrderNumber, [FromBody] CommodityDto model)
        {

            var res = await _orderManager.AddCommodityToOrderDetailAsync(OrderNumber, model);

            return res;

        }

        [HttpPut]
        [Route("UpdateCommodityInOrderDetail")]

        public async Task<Response> UpdateCommodityInOrderDetailAsyncAsync(string OrderNumber, [FromBody] CommodityDto model)
        {

            var res = await _orderManager.UpdateCommodityInOrderDetailAsync(OrderNumber, model);

            return res;

        }

        [HttpPut]
        [Route("UpdateShipmentInOrderDetail")]

        public async Task<Response> UpdateShipmentInOrderDetailAsync(string OrderNumber, [FromBody] List<ShipmentDto> model)
        {

            var res = await _orderManager.UpdateShipmentInOrderDetailAsync(OrderNumber, model);

            return res;

        }
    }
}
