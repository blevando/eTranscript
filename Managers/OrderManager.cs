using eTranscript.Models.DomainModels;
using eTranscript.Models.EntityModels;
using eTranscript.Services.Interfaces;

namespace eTranscript.Managers
{
    public class OrderManager
    {
        private readonly IInventoryManagement _order;

        public OrderManager(IInventoryManagement order)
        {
            _order = order;
        }

        public async Task<Response> CreateCategoryAsync(Category model)
        {
            var response = await _order.CreateCategoryAsync(model);
            return response;
        }

        public async Task<Response> CreateCommodityAsync(Commodity model)
        {
            var response = await _order.CreateCommodityAsync(model);
            return response;
        }

        public async Task<Response> CreateShipmentAsync(Shipment model)
        {
            var response = await _order.CreateShipmentAsync(model);
            return response;
        }

        public async Task<Response> DeleteCategoryAsync(int Id)
        {
            var response = await _order.DeleteCategoryAsync(Id);
            return response;
        }

        public async Task<Response> DeleteCommodityAsync(int Id)
        {
            var response = await _order.DeleteCommodityAsync(Id);
            return response;
        }

        public async Task<Response> DeleteShipmentAsync(int Id)
        {
            var response = await _order.DeleteShipmentAsync(Id);
            return response;
        }

        public async Task<Response> GetAllCategoryAsync()
        {
            var response = await _order.GetAllCategoryAsync();
            return response;
        }

        public async Task<Response> GetAllCommodityAsync()
        {
            var response = await _order.GetAllCommodityAsync();
            return response;
        }

        public async Task<Response> GetAllShipmentAsync()
        {
            var response = await _order.GetAllShipmentAsync();
            return response;
        }

        public async Task<Response> GetCategoryByIdAsync(int Id)
        {
            var response = await _order.GetCategoryByIdAsync(Id);
            return response;
        }

        public async Task<Response> GetCommodityByIdAsync(int Id)
        {
            var response = await _order.GetCommodityByIdAsync(Id);
            return response;
        }

        public async Task<Response> GetShipmentByIdAsync(int Id)
        {
            var response = await _order.GetShipmentByIdAsync(Id);
            return response;
        }

        public async Task<Response> UpdateCategoryAsync(Category model, int Id)
        {
            var response = await _order.UpdateCategoryAsync(model, Id);
            return response;
        }

        public async Task<Response> UpdateCommodityAsync(Commodity model, int Id)
        {
            var response = await _order.UpdateCommodityAsync(model, Id);
            return response;
        }

        public async Task<Response> UpdateShipmentAsync(Shipment model, int Id)
        {
            var response = await _order.UpdateShipmentAsync(model, Id);
            return response;
        }   
    }
}