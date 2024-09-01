﻿using eTranscript.Common.Utilities;
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
        /// <summary>
        /// Adding Shipments to the order detail: First delete existing (if any) and re-insert the collection of models
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> AddShipmentToOrderDetailAsync(string OrderNumber, List<ShipmentDto> model)
        {
            Response response = new Response();
            try
            {
                if (model.Count>0)
                {
                    // Delete the existing shipment Items in the OrderDetail Table

                    var shippmentsToRemove = _context.OrderDetail.Where(p => p.OrderNumber == OrderNumber && p.OrderItemType == OrderItemType.Shipment).ToList();

                    // Remove the entities that match the condition
                    _context.OrderDetail.RemoveRange(shippmentsToRemove);

                    // Save changes to the database
                    _context.SaveChanges();

                    // Then Add all items in the model collection above

                  List<OrderDetail> orderDetails = new List<OrderDetail>();

                    foreach (ShipmentDto item in model)
                    {
                        OrderDetail detail = new OrderDetail();
                        detail.OrderNumber = OrderNumber;
                        detail.Price = item.Price;
                        detail.Item = item.Name;
                        detail.OrderItemType = OrderItemType.Shipment;
                        orderDetails.Add(detail);                        

                    }
                    // Add the list of OrderDetail objects to the context
                   await _context.OrderDetail.AddRangeAsync(orderDetails);

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    response.Message = "Shipment Items inserted";
                    response.Code = 200;
                    response.Data = model.Count;                   

                }
                else
                {
                    response.Message = "No Item to insert";
                    response.Code = 201;
                    response.Data = model.Count;
                }
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


                    OrderNumber = CommonUtility.GetOrderNumberByCustomerId(customerId),

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
                // Also create the order details associated with this order
                //Insert the commodity ordered

                var orderDetail = await CreateOrderDetailByCommodityAsync(order.OrderNumber, model);

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

        public async Task<Response> CreateOrderDetailByCommodityAsync(string orderNumber, Commodity model)
        {
            Response response = new Response();
            try
            {
                // Check if the category already exists
                var existingOrderDetail = await _context.OrderDetail.AnyAsync(c => c.OrderNumber == orderNumber);

                if (existingOrderDetail != null)
                {

                    response.Message = "OrderDetail already exists";
                    response.Code = 200;
                    response.Data = existingOrderDetail;

                    return response;
                }

                // Create and add a new category
                var orderDetail = new OrderDetail
                {
                    OrderNumber = orderNumber,
                    Price = model.Price,
                    Item = model.Item

                };

                await _context.OrderDetail.AddAsync(orderDetail);
                await _context.SaveChangesAsync();

                response.Code = 200;
                response.Message = "OrderDetail created successfully";
                response.Data = orderDetail;
                // Also create the order details associated with this order
                //Insert the commodity ordered

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
