
namespace HappyCompany.WebApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUserDetails(string id);
        Task<UserDto> Login(LoginDto dto);
        Task<bool> Update(EditUserDto dto);
        Task<bool> changePassword(ChangePasswordDto dto);
        Task<bool> Delete(string userId);
    }
}
