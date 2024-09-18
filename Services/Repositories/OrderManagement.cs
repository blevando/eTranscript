using eTranscript.Common.Utilities;
using eTranscript.Data;
using eTranscript.Models.DomainModels;
using eTranscript.Models.EntityModels;
using eTranscript.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata;

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
                if (model.Count > 0)
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
                        detail.Address = item.Address;
                        detail.Item = item.Name;
                        detail.OrderItemType = OrderItemType.Shipment;
                        orderDetails.Add(detail);

                    }
                    // Add the list of OrderDetail objects to the context
                    await _context.OrderDetail.AddRangeAsync(orderDetails);

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Add invoice



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

        public async Task<Response> CreateInvoiceAsync(string OrderNumber)
        {
            Response response = new Response();
            try
            {
                // decimal totalUnitPrice = await _context.OrderDetail.Sum(od => od.Price && od.OrderNumber == OrderNumber);

                //  decimal totalUnitPriceForOrder = _context.OrderDetail.Where(od => od.OrderNumber == OrderNumber).Sum(od => od.Price);

                decimal documentSubtotal = _context.OrderDetail.Where(od => od.OrderNumber == OrderNumber && od.OrderItemType == OrderItemType.Document).Sum(od => od.Price);

                decimal shippingSubtotal = _context.OrderDetail.Where(od => od.OrderNumber == OrderNumber && od.OrderItemType == OrderItemType.Shipment).Sum(od => od.Price);

                decimal taxAmount = _context.OrderDetail.Where(od => od.OrderNumber == OrderNumber).Sum(od => od.Price);

                decimal totalAmount = documentSubtotal + shippingSubtotal;

                //Does it exist? 
                // NO: Insert
                // YES: Update

                //var existingInvoice = await _context.OrderDetail.AnyAsync(c => c.OrderNumber == OrderNumber);
                var existingInvoice = await _context.Invoice.FirstOrDefaultAsync(c => c.OrderNumber == OrderNumber);

                if (existingInvoice != null)
                {
                    // xists - Just update
                    existingInvoice.TotalAmount = totalAmount;
                    existingInvoice.TaxAmount = taxAmount;
                    existingInvoice.DocumentSubTotal = documentSubtotal;
                    existingInvoice.ShippmentSubtotal = shippingSubtotal;

                    _context.Entry(existingInvoice).State = EntityState.Modified;
                    //   _context.Invoice.Update(existingInvoice);

                    response.Message = "Invoice already exists";
                    response.Code = 200;
                    response.Data = existingInvoice;
                }
                else
                {
                    Invoice invoice = new Invoice();
                    invoice.OrderNumber = OrderNumber;
                    invoice.InvoiceNumber = CommonUtility.GetInvoiceNumber();
                    invoice.TotalAmount = totalAmount;
                    invoice.TaxAmount = taxAmount;
                    invoice.DocumentSubTotal = documentSubtotal;
                    invoice.ShippmentSubtotal = shippingSubtotal;
                    invoice.Status = 0; // pending payment
                    await _context.Invoice.AddAsync(invoice);

                    response.Message = "Invoice Created Successfully";
                    response.Code = 200;
                    response.Data = invoice;

                    // it is new - just insert
                }

                await _context.SaveChangesAsync();

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

        public async Task<Response> CreateOrderAsync(string customerId, CommodityDto model)
        {

            Response response = new Response();
            try
            {
                // Check if the category already exists
                var existingOrder = await _context.Order.FirstOrDefaultAsync(c => c.Status == 0 && c.CustomerId == customerId.Trim());
                string orderNumber = CommonUtility.GetOrderNumberByCustomerId(customerId);
                if (existingOrder != null)
                {

                    response.Message = "Order already exists";
                    response.Code = 200;

                    var oldOrder = await GetOrderByNumberAsync(existingOrder.OrderNumber);
                    response.Data = oldOrder;
                    orderNumber = existingOrder.OrderNumber;


                    return response;
                }

                // Create and add a new category
                var order = new Order
                {
                    CustomerId = customerId,


                    OrderNumber = orderNumber,

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

                // Adds the document to it
                var orderDetail = await AddCommodityToOrderDetailAsync(order.OrderNumber, model);


                //   var shippingDetail = await AddShipmentToOrderDetailAsync(order.OrderNumber, shipments);

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

        public async Task<Response> AddCommodityToOrderDetailAsync(string orderNumber, CommodityDto model)
        {
            Response response = new Response();
            try
            {
                // Check if the category already exists
                var existingOrderDetail = await _context.OrderDetail.AnyAsync(c => c.OrderNumber == orderNumber);

                if (existingOrderDetail != false)
                {

                    response.Message = "OrderDetail already exists";
                    response.Code = 200;

                 


                    response.Data = existingOrderDetail;

                    // Update the order Details




                    return response;
                }

                // Create and add a new category
                var orderDetail = new OrderDetail
                {
                    OrderNumber = orderNumber,
                    Price = model.Price,
                    Item = model.Item,
                    OrderItemType = OrderItemType.Document,
                    Address = "Pickup by hand",




                };

                await _context.OrderDetail.AddAsync(orderDetail);
                await _context.SaveChangesAsync();

                // Add shipment 

                if (model.Shipment.Count > 0)
                {
                    var shipmentDetail = await AddShipmentToOrderDetailAsync(orderNumber, model.Shipment);

                    var newInvoice = await CreateInvoiceAsync(orderNumber);
                }
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

        public async Task<Response> UpdateCommodityInOrderDetailAsync(string orderNumber, CommodityDto model)
        {

            Response response = new Response();
            //var existingInvoice = await _context.OrderDetail.AnyAsync(c => c.OrderNumber == OrderNumber);
            var existingCommodity = await _context.OrderDetail.FirstOrDefaultAsync(c => c.OrderNumber == orderNumber && c.OrderItemType == OrderItemType.Document);
            OrderDetail detail = new OrderDetail();

            if (existingCommodity != null)
            {

                detail.OrderNumber = orderNumber;
                detail.Id = existingCommodity.Id;
                detail.Address = existingCommodity.Address;
                detail.Price = existingCommodity.Price;
                detail.Item = existingCommodity.Item;
                detail.OrderItemType = existingCommodity.OrderItemType;


                _context.Entry(detail).State = EntityState.Modified;
                //   _context.Invoice.Update(existingInvoice);

                response.Message = "Updated already exists";
                response.Code = 200;
                response.Data = detail;
            }
            else
            {

                detail.OrderNumber = orderNumber;
                detail.Id = existingCommodity.Id;
                detail.Address = existingCommodity.Address;
                detail.Price = existingCommodity.Price;
                detail.Item = existingCommodity.Item;
                detail.OrderItemType = existingCommodity.OrderItemType;

                _context.OrderDetail.Add(detail);
                response.Message = "Added Commodity to exists orderdetail";
                response.Code = 200;
                response.Data = detail;

                // it is new - just insert
            }

            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<Response> UpdateShipmentInOrderDetailAsync(string OrderNumber, List<ShipmentDto> model)
        {

            Response response = new Response();
            try
            {
                if (model.Count > 0)
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
                        detail.Address = item.Address;
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
        public Task<Response> CreateReceiptAsync(string OrderNumber, decimal TotalPrice)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteShipmentFromOrderDetailAsync(string OrderNumber, Shipment model)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> GetOrderByNumberAsync(string orderNumber)
        {

            var orderToRetrieve = await _context.Order.FirstOrDefaultAsync(p => p.OrderNumber == orderNumber);

            Response response = new Response();

            if (orderToRetrieve != null)
            {
                // The order exists
                // Then copy the order attributes into the consolidated object
                ConsolidatedOrderDto consolidated = new ConsolidatedOrderDto();

               
                consolidated.CustomerName = orderNumber;
                consolidated.CustomerId = orderNumber;

                consolidated.OrderNumber = orderToRetrieve.OrderNumber;
                consolidated.PaymentMethod = orderToRetrieve.PaymentMethod;
                consolidated.PaymentReference = orderToRetrieve.PaymentReference;
                consolidated.PaymentGateway = orderToRetrieve.PaymentGateway;
                

                consolidated.InvoiceDto = new InvoiceDto ();
                consolidated.DocumentDto = new DocumentDto();
                consolidated.ShipmentDto = new List<ShipmentDto>();

                // Get a list of orderdetails
                List<OrderDetail> orderDetails = _context.OrderDetail.Where(p => p.OrderNumber == orderNumber).ToList();

                if (orderDetails != null)
                {
                    foreach (var orderDetail in orderDetails)
                    {
                        switch (orderDetail.OrderItemType)
                        {
                            case OrderItemType.Document:
                                consolidated.DocumentDto.Price = orderDetail.Price;
                                consolidated.DocumentDto.Item = orderDetail.Item;
                                 consolidated.DocumentDto.Address = orderDetail.Address;

                                break;

                            case OrderItemType.Shipment:

                                ShipmentDto shipment = new ShipmentDto();
                                shipment.Address = orderDetail.Address;
                                shipment.Price = orderDetail.Price;
                                shipment.Name = orderDetail.Item;
                                consolidated.ShipmentDto.Add(shipment);

                                break;
                            default:

                                break;
                        }
                    }
                }

                // Add invoice here

                    var invoices = await _context.Invoice.FirstOrDefaultAsync(p => p.OrderNumber == orderNumber);

                if (invoices != null)
                {

                    consolidated.InvoiceDto.Status = invoices.Status;
                    consolidated.InvoiceDto.OrderNumber = invoices.OrderNumber;
                    consolidated.InvoiceDto.InvoiceNumber = invoices.InvoiceNumber;
                    consolidated.InvoiceDto.ShippmentSubtotal = invoices.ShippmentSubtotal;
                    consolidated.InvoiceDto.DocumentSubTotal = invoices.DocumentSubTotal;
                    consolidated.InvoiceDto.TaxAmount = invoices.TaxAmount;
                    consolidated.InvoiceDto.TotalAmount = invoices.TotalAmount;

                }
              

                response.Message = "Success";
                 response.Code = 200;
                response.Data = consolidated;


            }
            else
            {
                response.Message = "No data found";
                response.Code = 404;
                response.Data = null;
            }


            return response;

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

        public async Task<Response> DeleteOrderAsync(string OrderNumber)
        {
            Response response = new Response();



            // Check if it exists
            var orderToRemove = await _context.Order.FirstOrDefaultAsync(p => p.OrderNumber == OrderNumber);

            if (orderToRemove != null)
            {
                // 1. Delete the Invoice
                var invoiceToRemove = await _context.Invoice.FirstOrDefaultAsync(p => p.OrderNumber == OrderNumber);
                if (invoiceToRemove != null)
                {
                    _context.Invoice.Remove(invoiceToRemove);
                    await _context.SaveChangesAsync();
                }

                //2. Delete the OrderDetail

                var orderdetailToRemove =  await _context.OrderDetail.Where(p => p.OrderNumber == OrderNumber).ToListAsync();
                if (orderdetailToRemove != null)
                {
                    _context.OrderDetail.RemoveRange(orderdetailToRemove);
                    await _context.SaveChangesAsync();
                }
                //3. Delete the Order


                _context.Order.Remove(orderToRemove);
                await _context.SaveChangesAsync();


                response.Data = orderToRemove;
                response.Code = 200;
                response.Message = "Successfully removed an order";
            }
            else
            {
                response.Data = null;
                response.Code = 404;
                response.Message = "Order does not exists";
            }


            return response;
        }

        //public Task<Response> ProceedToPaymentAsync(string orderNumber)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
