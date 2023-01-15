using HappyCompany.Domain.Data;

namespace HappyCompany.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : BaseApiController
    {
        public WarehouseController(IWarehouseServiceHandler warehouseService, ILogger logger):base(warehouseService, logger)
        {
                
        }

        [HttpGet("/list")]
        public async Task <IActionResult> GetAsync([FromQuery] PaginationQueryDto pagination)
        {
            _logger.Information($"User Invokes To Get All Warehouses");

            var response = await _waehouseServiceHandler.GetWarehouseListPage(pagination);
            return SendAppropriateResponse(response);

        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            _logger.Information($"User Invokes To Get one Warehouse with id = {id}");

            var response = await _waehouseServiceHandler.GetWarehouseDetails(id);
            return SendAppropriateResponse(response);
        }


        [HttpPost("warehouse/Add")]
        public async Task<IActionResult> CreateAsync(WarehouseDto entity)
        {
            _logger.Information($"User Invokes To Get add new Warehouse with name = {entity.Name}");

            var response = await _waehouseServiceHandler.AddWarehouse(entity);
            return SendAppropriateResponse(response);
        }
    }
}
