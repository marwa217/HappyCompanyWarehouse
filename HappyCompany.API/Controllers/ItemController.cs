
using HappyCompany.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace HappyCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : BaseApiController
    {
        public ItemController(IItemServiceHandler itemService, ILogger logger) : base(itemService, logger)
        {

        }

        [HttpGet("list/{WarehouseId}")]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationQueryDto pagination, int WarehouseId)
        {
            _logger.Information($"User Invokes To Get All Items Related to Warehouse id = {WarehouseId}");

            var response = await _itemServiceHandler.GetItemListPage(pagination,WarehouseId);
            return SendAppropriateResponse(response);
        }



        [HttpPost("item/add")]
        public async Task<IActionResult> CreateAsync(Item entity)
        {
            _logger.Information($"User Invokes To Get add new Warehouse with name = {entity.Name}");

            var response = await _itemServiceHandler.AddItem(entity);
            return SendAppropriateResponse(response);
        }
    }
}
