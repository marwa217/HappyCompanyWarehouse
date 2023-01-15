using HappyCompany.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace HappyCompany.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        public AccountController(IUserServiceHandler userServiceHandler, ILogger logger) : base(userServiceHandler, logger)
        {

        }
        [HttpGet("/listUsers")]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.Information($"User Invokes To Get All Users");

            var response = await _userServiceHandler.GetUsersList();
            return SendAppropriateResponse(response);
        }

        [HttpGet("user/{Id}")]
        public async Task<IActionResult> DetailsAsync(string Id)
        {
            _logger.Information($"User Invokes To Get one User");

            var response = await _userServiceHandler.GetUserDetails(Id);
            return SendAppropriateResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            _logger.Information($"User Invokes To Login With Params {dto.ToString()}", dto);

            var response = await _userServiceHandler.Login(dto);
            return SendAppropriateResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public async Task<IActionResult> AddUserAsync(RegisterDto dto)
        {
            _logger.Information($"User Invokes To add user With Params {dto.ToString()}", dto);

            var response = await _userServiceHandler.AddUser(dto);
            return SendAppropriateResponse(response);
        }

        [HttpPut("/changePassword")]
        public async Task<IActionResult> changePassword(ChangePasswordDto dto)
        {
            _logger.Information($"User Invokes To changePassword With Params {dto.ToString()}", dto);

            var response = await _userServiceHandler.ChangePassword(dto);
            return SendAppropriateResponse(response);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateAsync(EditUserDto user, string id)
        {
            _logger.Information($"User Invokes To update his info With Id= {id}", user);

            var response = await _userServiceHandler.UpdateUser(user);
            return SendAppropriateResponse(response);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            _logger.Information($"User Invokes To changePassword With Params {id.ToString()}", id);

            var response = await _userServiceHandler.DeleteUser(id);
            return SendAppropriateResponse(response);
        }
    }
}
