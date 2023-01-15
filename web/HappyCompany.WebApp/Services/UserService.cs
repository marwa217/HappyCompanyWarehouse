using HappyCompany.WebApp.Services.Helper;
using HappyCompany.WebApp.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace HappyCompany.WebApp.Services
{
    public class UserService : HttpClientHelper, IUserService
    {
        public UserService(HttpClient httpClient) : base(httpClient)
        {

        }
        
        public async Task<bool> changePassword(ChangePasswordDto dto)
        {
            if(_userDto.token != null)
            {
                await _httpClient.PutAsJsonAsync("changePassword", dto);
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(string userId)
        {
            if (_userDto.token != null)
            {
                await _httpClient.DeleteAsync($"api/Account/delete/{userId}");
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<UserDto[]>("listUsers");
        }

        public async Task<UserDto> GetUserDetails(string Id)
        {
            if (_userDto.token != null)
            {
                return await _httpClient.GetFromJsonAsync<UserDto>($"user/{Id}");

            }
            return null;
        }
        public async Task<UserDto> Login(LoginDto dto)
        {
            
            var response = await _httpClient.PostAsJsonAsync("login", dto);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                _userDto = await JsonSerializer.DeserializeAsync<UserDto>(responseStream);
                PrepareHeader();
                return _userDto;
            }
            return null;
        }

        public async Task<bool> Update(EditUserDto dto)
        {
            if (_userDto.token != null)
            {
                await _httpClient.PutAsJsonAsync($"api/Account/update/{dto.Id}", dto);
                return true;
            }
            return false;
        }
    }
}
