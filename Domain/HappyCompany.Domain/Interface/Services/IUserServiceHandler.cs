using HappyCompany.Domain.Dtos;

namespace HappyCompany.Domain.Interface.Services
{
    public interface IUserServiceHandler
    {
        Task<Response> Login(LoginDto loginDto);
        Task<Response> GetUsersList();
        Task<Response> ChangePassword(ChangePasswordDto passwordDto);
        Task<Response> UpdateUser(EditUserDto user);
        Task<Response> DeleteUser(string userId);
        Task<Response> GetUserDetails(string userId);
        Task<Response> AddUser(RegisterDto user);
    }
}
    