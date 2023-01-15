using HappyCompany.Domain.CustomizedResponses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using HttpResponse = HappyCompany.Infra.Constants.HttpResponse;


namespace HappyCompany.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseApiController : ControllerBase
    {
        protected IItemServiceHandler _itemServiceHandler;
        protected IWarehouseServiceHandler _waehouseServiceHandler;
        protected IUserServiceHandler _userServiceHandler;
        protected ILogger _logger;

        //TODO: Add Logger
        public BaseApiController(IItemServiceHandler itemServiceHandler, ILogger logger)
        {
            _itemServiceHandler = itemServiceHandler;
            _logger = logger;
        }

        public BaseApiController(IWarehouseServiceHandler waehouseServiceHandler, ILogger logger)
        {
            _waehouseServiceHandler = waehouseServiceHandler;
            _logger = logger;
                
        }
        public BaseApiController(IUserServiceHandler userServiceHandler, ILogger logger)
        {
            _userServiceHandler = userServiceHandler;
            _logger = logger;
        }

        public BaseApiController()
        {
                
        }

        protected IActionResult SendAppropriateResponse(Response response)
        {
            switch (response.HttpResponse)
            {
                case HttpResponse.BadRequest:
                    return BadRequest(response.Message);
                case HttpResponse.NotFound:
                    return NotFound(response.Message);
                case HttpResponse.Unauthorized:
                    return Unauthorized(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
