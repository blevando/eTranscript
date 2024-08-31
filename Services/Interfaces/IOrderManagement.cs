using eTranscript.Models.DomainModels;
using eTranscript.Models.EntityModels;
using System.Runtime.InteropServices;

namespace eTranscript.Services.Interfaces
{
    public interface IOrderManagement
    { 

        Task<Response> CreateOrderAsync(Commodity model); //  1
                                                         
        Task<Response> CreateOrderDetailByCommodityAsync(string orderNumber, Commodity model); //  2
        Task<Response> AddShipmentToOrderDetailAsync(string OrderNumber, Shipment model); //  3
        Task<Response> DeleteShipmentFromOrderDetailAsync(string OrderNumber, Shipment model); //  3

        Task<Response> CreateInvoiceAsync(string OrderNumber); //  4

        Task<Response>   GetTotalPriceFromOrderDetailsByOrderNumberAsync(string OrderNumber); //

        Task<Response> UpdateInvoiceAsync(string OrderNumber, bool PaymentStatus); //  4

        Task<Response> CreateReceiptAsync(string OrderNumber, decimal TotalPrice); // 5
        Task<Response> UpdateReceiptAsync(string OrderNumber, decimal TotalPrice, bool Status);

        Task<Response> UpdateOrderAsync(string OrderNumber, string PaymentGateWay, string PaymentMethod, string PaymentReference,string Note, bool PaymentStatus );
        Task<Response> GetOrderByNumberAsync(string orderNumber);
 
       

    }
}
