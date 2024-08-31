using eTranscript.Data;
using eTranscript.Models.DomainModels;
using eTranscript.Models.EntityModels;
using eTranscript.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;

namespace eTranscript.Services.Repositories
{
    public class OrderManagement : IInventoryManagement
    {
        private readonly ApplicationDbContext _context;
        public OrderManagement(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> CreateCategoryAsync(Category model)
        {
            var response = new Response();

            try
            {
                // Check if the category already exists
                var existingCategory = await _context.Category
                    .AnyAsync(c => c.Description == model.Description);

                if (existingCategory)
                {
                    response.Message = "Category already exists";
                    response.Code = 400;
                    response.Data = null;

                    return response;
                }

                // Create and add a new category
                var newCategory = new Category
                {
                    Description = model.Description
                };

                await _context.Category.AddAsync(newCategory);
                await _context.SaveChangesAsync();

                response.Code = 200;
                response.Message = "Category created successfully";
                response.Data = newCategory;
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

        public async Task<Response> CreateCommodityAsync(Commodity model)
        {
            var response = new Response();

            try
            {
                // Check if the commodity already exists
                var existingCommodity = await _context.Commodity
                    .AnyAsync(c => c.CategoryId == model.CategoryId && c.Item==model.Item && c.Price==model.Price);

                if (existingCommodity)
                {
                    response.Message = "Commodity already exists";
                    response.Code = 400;
                    response.Data = null;

                    return response;
                }

                // Create and add a new commodity
                var newCommodity = new Commodity
                {
                    CategoryId = model.CategoryId,
                    Item = model.Item,
                    Price=model.Price

                };

                await _context.Commodity.AddAsync(newCommodity);
                await _context.SaveChangesAsync();

                response.Code = 200;
                response.Message = "Commodity created successfully";
                response.Data = newCommodity;
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

        public async Task<Response> CreateShipmentAsync(Shipment model)
        {
            var response = new Response();

            try
            {
                // Check if the Shipent already exists
                var existingShipment = await _context.Shipment
                    .AnyAsync(c => c.Name == model.Name && c.Price == model.Price && c.Status == model.Status);

                if (existingShipment)
                {
                    response.Message = "Shipment already exists";
                    response.Code = 400;
                    response.Data = null;

                    return response;
                }

                // Create and add a new shipment
                var newShipment = new Shipment
                {
                    Name = model.Name,
                    Price = model.Price,
                    Status = model.Status

                };

                await _context.Shipment.AddAsync(newShipment);
                await _context.SaveChangesAsync();

                response.Code = 200;
                response.Message = "Shipment created successfully";
                response.Data = newShipment;
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

        public async Task<Response> DeleteCategoryAsync(int Id)
        {
            var response = new Response();
            try
            {
                var existingCategory = await _context.Category.FirstOrDefaultAsync(a => a.Id == Id);
                if(existingCategory !=null)
                {
                    _context.Category.Remove(existingCategory);
                    await _context.SaveChangesAsync();

                    response.Message = "Category Successfully Deleted";
                    response.Code = 200;
                }
                else
                {
                    response.Message = "Category not Found";
                    response.Code = 404;
                }
            }
            catch(Exception ex)
            {
                response.Message = $"Error:{ex.Message}";
                response.Code = 500;
            }

            return response;
        }

        public async Task<Response> DeleteCommodityAsync(int Id)
        {
            var response = new Response();

            try
            {
                var existingCommodity = await _context.Commodity.FirstOrDefaultAsync(a => a.Id == Id);
                if(existingCommodity !=null)
                {
                    _context.Commodity.Remove(existingCommodity);
                    await _context.SaveChangesAsync();

                    response.Message = "Commodity Deleted Successfully";
                    response.Code = 200;
                    response.Data = existingCommodity;
                }
                else
                {
                    response.Message = "Commodity not found";
                    response.Code = 404;
                    response.Data = null;
                }
            }
            catch(Exception ex)
            {
                response.Message = $"Error:{ex.Message}";
                response.Code = 500;
                response.Data = null;
            }

            return response;
        }

        public async Task<Response> DeleteShipmentAsync(int Id)
        {
            var response = new Response();

            try
            {
                var existShipment = await _context.Shipment.FirstOrDefaultAsync(a => a.id == Id);
                if(existShipment !=null)
                {
                    _context.Shipment.Remove(existShipment);
                    await _context.SaveChangesAsync();

                    response.Message = "Shipment Successfully Deleted";
                    response.Code = 200;
                    response.Data = existShipment;
                }

                else
                {
                    response.Message = "Shipment not Found";
                    response.Code = 400;
                    response.Data = null;
                }

            }
            catch(Exception ex)
            {
                response.Message = $"Error:{ex.Message}";
                response.Code = 500;
                response.Data = null;
            }

            return response;
        }

        public async Task<Response> GetAllCategoryAsync()
        {
            var response = new Response();
            try
            {
                var categoryItem = await _context.Category.ToListAsync();

                response.Message = "Successfully";
                response.Code = 200;
                response.Data = categoryItem;

                return response;
            }
            catch(Exception ex)
            {
                response.Message = $"Error:{ex.Message}";
                response.Code = 500;
                response.Data = null;
            }
            return response;
        }

        public async Task<Response> GetAllCommodityAsync()
        {
            var response = new Response();
            try
            {

                var existingCommodity = await _context.Commodity.ToListAsync();

                response.Message = "Successfully";
                response.Code = 200;
                response.Data = existingCommodity;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Error:{ex.Message}";
                response.Code = 500;
                response.Data = null;
            }

            return response;
        }

        public async Task<Response> GetAllShipmentAsync()
        {
            var response = new Response();

            try
            {
                var exitingShipment = await _context.Shipment.ToListAsync();

                response.Message = "Successful";
                response.Code = 200;
                response.Data = exitingShipment;

                return response;
            }
            catch(Exception ex)
            {
                response.Message = $"Error:{ex.Message}";
                response.Code = 500;
                response.Data = null;
            }

            return response;
        }

        public async Task<Response> GetCategoryByIdAsync(int Id)
        {
           var response = new Response();

            try
            {
                var existingCategory = await _context.Category.Where(a => a.Id == Id).FirstOrDefaultAsync();
                if(existingCategory != null)
                {
                    response.Message = "Successful";
                    response.Code = 200;
                    response.Data = existingCategory;

                    return response;
                }
                else
                {
                    response.Message = "Category not found";
                    response.Code = 404;
                    response.Data = null;
                }
            }
            catch(Exception ex)
            {
                response.Message = $"Error:{ex.Message}";
                response.Code = 500;
                response.Data = null;
            }

            return response;
        }

        public async Task<Response> GetCommodityByIdAsync(int Id)
        {
            var response = new Response();

            try
            {
                var checkcommodity = await _context.Commodity.Where(a => a.Id == Id).FirstOrDefaultAsync();
                if(checkcommodity !=null)
                {
                    response.Message = "Successfull";
                    response.Code = 200;
                    response.Data = checkcommodity;

                    return response;
                }
                else
                {
                    response.Message = "Commodity not found";
                    response.Code = 404;
                    response.Data = null;
                }
            }
            catch(Exception ex)
            {
                response.Message = $"Error:{ex.Message}";
                response.Code = 500;
                response.Data = null;
            }

            return response;
        }

        public async Task<Response> GetShipmentByIdAsync(int Id)
        {
            var response = new Response();

            try
            {
                var checkshipment = await _context.Shipment.Where(a => a.id == Id).FirstOrDefaultAsync();
                if(checkshipment !=null)
                {
                    response.Message = "Successful";
                    response.Code = 200;
                    response.Data = checkshipment;

                    return response;
                }
                else
                {
                    response.Message = "Shipment not Found";
                    response.Code = 404;
                    response.Data = null;
                }
            }
            catch(Exception ex)
            {
                response.Message = $"Error:{ex.Message}";
                response.Code = 500;
                response.Data = null;
            }

            return response;
        }

        public async Task<Response> UpdateCategoryAsync(Category model, int Id)
        {
            var response = new Response();

            try
            {
                var checkcategory = await _context.Category.FirstOrDefaultAsync(a => a.Id == Id);
                if(checkcategory !=null)
                {
                    checkcategory.Description = model.Description;

                    _context.Entry(checkcategory).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    response.Message = "Successful";
                    response.Code = 200;
                    response.Data = checkcategory;

                    return response;

                }
                else
                {
                    response.Message = "Category not found";
                    response.Code = 404;
                    response.Data = null;
                }
            }
            catch(Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
                response.Code = 500;
                response.Data = null;
            }

            return response;
        }

        public async Task<Response> UpdateCommodityAsync(Commodity model, int Id)
        {
            var response = new Response();
            try
            {
                var checkCommodity = await _context.Commodity.FirstOrDefaultAsync(a => a.Id == Id);
                if (checkCommodity != null)
                {
                    checkCommodity.CategoryId = model.CategoryId;
                    checkCommodity.Item = model.Item;
                    checkCommodity.Price = model.Price;

                    _context.Entry(checkCommodity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    response.Message = "Successful";
                    response.Code = 200;
                    response.Data = checkCommodity;

                    return response;


                }

                else
                {
                    response.Message = "CheckCommodity is not found";
                    response.Code = 404;
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error:{ex.Message}";
                response.Code = 500;
                response.Data = null;
            }

            return response;
        }

        public async Task<Response> UpdateShipmentAsync(Shipment model, int Id)
        {
            var response = new Response();

            try
            {
                var checkShipment = await _context.Shipment.FirstOrDefaultAsync(a => a.id == Id);
                if(checkShipment !=null)
                {
                    response.Message = "Successfull";
                    response.Code = 200;
                    response.Data = checkShipment;
                }
                else
                {
                    response.Message = "Shipment not Found";
                    response.Code = 404;
                    response.Data = null;
                }
            }
            catch(Exception ex)
            {
                response.Message = $"Error:{ex.Message}";
                response.Code = 500;
                response.Data = null;     
            }

            return response;
        }
    }
}
