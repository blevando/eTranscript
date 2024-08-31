using eTranscript.Managers;
using eTranscript.Models.EntityModels;
using eTranscript.Models.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eTranscript.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryManagementController : ControllerBase
    {
        private readonly InventoryManager _manager;
        public InventoryManagementController(InventoryManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategoryAsync([FromQuery] Category model)
        {
            //var response = new Response();

            try
            {
                var category = await _manager.CreateCategoryAsync(model);
                if (category != null)
                {
                    return Ok(category);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }

        }

        [HttpPost]
        [Route("CreateCommodity")]
        public async Task<IActionResult> CreateCommodityAsync([FromQuery] Commodity model)
        {
            try
            {
                var commodity = await _manager.CreateCommodityAsync(model);
                if (commodity != null)
                {
                    return Ok(commodity);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }

        }
        [HttpPost]
        [Route("CreateShipment")]
        public async Task<IActionResult> CreateShipmentAsync([FromQuery] Shipment model)
        {
            try
            {
                var shipment = await _manager.CreateShipmentAsync(model);
                if (shipment != null)
                {
                    return Ok(shipment);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }

        }

        [HttpGet]
        [Route("GetAllCategory")]

        public async Task<IActionResult> GetAllCategoryAsync()
        {
            try
            {
                var category = await _manager.GetAllCategoryAsync();
                if (category != null)
                {
                    return Ok(category);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }

        [HttpGet]
        [Route("GetAllCommodity")]

        public async Task<IActionResult> GetAllCommodityAsync()
        {
            try
            {
                var commodity = await _manager.GetAllCommodityAsync();
                if (commodity != null)
                {
                    return Ok(commodity);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }

        [HttpGet]
        [Route("GetAllShipment")]

        public async Task<IActionResult> GetAllShipmentAsync()
        {
            try
            {
                var shipment = await _manager.GetAllShipmentAsync();
                if (shipment != null)
                {
                    return Ok(shipment);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }

        [HttpGet]
        [Route("GetCategoryById")]

        public async Task<IActionResult> GetCategoryByIdAsync(int Id)
        {
            try
            {
                var category = await _manager.GetCategoryByIdAsync(Id);
                if (category != null)
                {
                    return Ok(category);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }
        [HttpGet]
        [Route("GetCommodityById")]

        public async Task<IActionResult> GetCommodityByIdAsync(int Id)
        {
            try
            {
                var commodity = await _manager.GetCommodityByIdAsync(Id);
                if (commodity != null)
                {
                    return Ok(commodity);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }

        [HttpGet]
        [Route("GetShipmentById")]

        public async Task<IActionResult> GetShipmentByIdAsync(int Id)
        {
            try
            {
                var category = await _manager.GetShipmentByIdAsync(Id);
                if (category != null)
                {
                    return Ok(category);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }

        [HttpPut]
        [Route("UpdateCategory")]

        public async Task<IActionResult> UpdateCategoryAsync(Category model, int Id)
        {
            try
            {
                var category = await _manager.UpdateCategoryAsync(model, Id);
                if (category != null)
                {
                    return Ok(category);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }

        [HttpPut]
        [Route("UpdateCommodity")]

        public async Task<IActionResult> UpdateCommodityAsync(Commodity model, int Id)
        {
            try
            {
                var commodity = await _manager.UpdateCommodityAsync(model, Id);
                if (commodity != null)
                {
                    return Ok(commodity);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }

        [HttpPut]
        [Route("UpdateShipment")]
        public async Task<IActionResult> UpdateShipmentAsync(Shipment model, int Id)
        {
            try
            {
                var shipment = await _manager.UpdateShipmentAsync(model, Id);
                if (shipment != null)
                {
                    return Ok(shipment);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }

        [HttpDelete]
        [Route("DeleteCategory")]

        public async Task<IActionResult> DeleteCategoryAsync(int Id)
        {
            try
            {
                var category = await _manager.DeleteCategoryAsync(Id);
                if (category != null)
                {
                    return Ok(category);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }

        [HttpDelete]
        [Route("DeleteCommodity")]

        public async Task<IActionResult> DeleteCommodityAsync(int Id)
        {
            try
            {
                var commodity = await _manager.DeleteCommodityAsync(Id);
                if (commodity != null)
                {
                    return Ok(commodity);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }

        [HttpDelete]
        [Route("DeleteShipment")]

        public async Task<IActionResult> DeleteShipmentAsync(int Id)
        {
            try
            {
                var shipment = await _manager.DeleteShipmentAsync(Id);
                if (shipment != null)
                {
                    return Ok(shipment);

                }
                else
                {
                    return NotFound(new Models.DomainModels.Response
                    {
                        Message = "Error try again later",
                        Code = 404,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.DomainModels.Response
                {
                    Message = $"Internal server error,{ex.Message}",
                    Code = 500,
                    Data = null
                });
            }
        }
    }
}
