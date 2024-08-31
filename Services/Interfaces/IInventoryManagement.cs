using eTranscript.Models.DomainModels;
using eTranscript.Models.EntityModels;

namespace eTranscript.Services.Interfaces
{
    public interface IInventoryManagement
    {
        // Category
        Task<Response> GetAllCategoryAsync();
        Task<Response> GetCategoryByIdAsync(int Id);
        Task<Response> CreateCategoryAsync(Category model);
        Task<Response> UpdateCategoryAsync(Category model, int Id);
        Task<Response> DeleteCategoryAsync(int Id);

        //Commodity
        Task<Response> GetAllCommodityAsync();
        Task<Response> GetCommodityByIdAsync(int Id);
        Task<Response> CreateCommodityAsync(Commodity model);
        Task<Response> UpdateCommodityAsync(Commodity model, int Id);
        Task<Response> DeleteCommodityAsync(int Id);

        //Shipment
        Task<Response> GetAllShipmentAsync();
        Task<Response> GetShipmentByIdAsync(int Id);
        Task<Response> CreateShipmentAsync(Shipment model);
        Task<Response> UpdateShipmentAsync(Shipment model, int Id);
        Task<Response> DeleteShipmentAsync(int Id);
    }
}
