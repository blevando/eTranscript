using eTranscript.Data;
using eTranscript.Models.DomainModels;
using eTranscript.Models.EntityModels;
using eTranscript.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTranscript.Services.Repositories
{
    public class OrderManagement : IOrderManagement
    {
        private readonly ApplicationDbContext _context;

        public OrderManagement(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Response> AddShipmentToOrderDetailAsync(string OrderNumber, Shipment model)
        {
            throw new NotImplementedException();
        }

        public Task<Response> CreateInvoiceAsync(string OrderNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> CreateOrderAsync(string customerId, Commodity model)
        {
            
            Response response = new Response();
            try
            {
                // Check if the category already exists
                var existingOrder = await _context.Order.AnyAsync(c => c.Status == 0 && c.CustomerId == customerId.Trim());
                
                if (existingOrder != null)
                {

                    response.Message = "Order already exists";
                    response.Code = 200;
                    response.Data = existingOrder;

                    return response;
                }

                // Create and add a new category
                var order = new Order
                {
                    CustomerId = customerId,


                    OrderNumber = "1233333333",

                    OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),

                    PaymentGateway = string.Empty,
                    PaymentMethod = string.Empty,

                    PaymentReference = string.Empty,

                    Status = 0,
                    Note = "wish list"
                };

                await _context.Order.AddAsync(order);
                await _context.SaveChangesAsync();

                response.Code = 200;
                response.Message = "Order created successfully";
                response.Data = order;

                return response;
            }
            catch (DbUpdateException dbEx)
            {
               
                response.Code = 500;
                response.Message = $"Database update error: {dbEx.Message}";
                response.Data = null;
            }
            catch (Exception ex)
            {
                 
                response.Code = 500;
                response.Message = $"An error occurred: {ex.Message}";
                response.Data = null;
            }

            return response;
        }

        public Task<Response> CreateOrderDetailByCommodityAsync(string orderNumber, Commodity model)
        {
            throw new NotImplementedException();
        }

        public Task<Response> CreateReceiptAsync(string OrderNumber, decimal TotalPrice)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteShipmentFromOrderDetailAsync(string OrderNumber, Shipment model)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetOrderByNumberAsync(string orderNumber)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetTotalPriceFromOrderDetailsByOrderNumberAsync(string OrderNumber)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateInvoiceAsync(string OrderNumber, bool PaymentStatus)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateOrderAsync(string OrderNumber, string PaymentGateWay, string PaymentMethod, string PaymentReference, string Note, bool PaymentStatus)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateReceiptAsync(string OrderNumber, decimal TotalPrice, bool Status)
        {
            throw new NotImplementedException();
        }
    }
}
