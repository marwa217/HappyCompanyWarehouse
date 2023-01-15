using AutoMapper;
using HappyCompany.Domain.Dtos;
using HappyCompany.Infra.Utils.Interfaces;
using Microsoft.AspNetCore.Identity;
using HappyCompany.Infra.Constants;

namespace HappyCompany.Application.Services
{
    public class UserServiceHandler : IUserServiceHandler
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly ILogger _logger;
        private readonly IJWTUtils _jWTUtils;

        public UserServiceHandler(IMapper mapper, ILogger logger,
            UserManager<User> userManager, IJWTUtils jWTUtils, RoleManager<IdentityRole> roleManger)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _jWTUtils = jWTUtils;
            _roleManger = roleManger;
        }

        public async Task<Response> GetUserDetails( string id)
        {
            if (id == null)
                throw new ArgumentNullException("string object cannot be empty ");
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
                return new Response
                {
                    HttpResponse = HttpResponse.NotFound,
                    Message = $"there isn't users with Id= {id}",
                };
            var userDto = _mapper.Map<UserDto>(user);
            var rolUsers = await _userManager.GetRolesAsync(user);
            userDto.userRoles = rolUsers.FirstOrDefault();

            return new Response
            {
                HttpResponse = HttpResponse.Ok,
                Message = "returned an user successfully ",
                Data = userDto,
                Succeeded = true
            };

        }

        public async Task<Response> AddUser(RegisterDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException("userDto object cannot be empty ");
            var user = new User()
            {
                Email = userDto.Email,
                FullName = userDto.fullname,
                UserName = userDto.fullname
            };
            var result = await _userManager.CreateAsync(user,userDto.Password);
            if (result.Succeeded)
            {
                var roleId = Roles.UserRoles.FirstOrDefault(x => x.Value == userDto.RoleName).Key;
                var role = await _roleManger.FindByIdAsync(roleId);
                if(role != null)
                    await _userManager.AddToRoleAsync(user, userDto.RoleName);
                _logger.Information($"User with name = {userDto.fullname} is add now");

                return new Response
                {
                    HttpResponse = HttpResponse.Ok,
                    Message = "added user successfully ",
                    Data = user,
                    Succeeded = true
                };
            }

            return new Response
            {
                HttpResponse = HttpResponse.InternalServerErrror,
                Message = "an error occured during delete User",
                Data = result.Errors,
                Succeeded = result.Succeeded
            };
        }

        public async Task<Response> ChangePassword(ChangePasswordDto passwordDto)
        {
            if(passwordDto == null)
                throw new ArgumentNullException(nameof(passwordDto));

            var user = await _userManager.FindByIdAsync(passwordDto.userId);
            if(user == null)
                return new Response
                {
                    HttpResponse = HttpResponse.NotFound,
                    Message = $"there isn't users with Id= {passwordDto.userId}",
                };
            _logger.Information($"User with id = {passwordDto.userId} is valid");

            var result = await _userManager.ChangePasswordAsync(user,
           passwordDto.oldPassword, passwordDto.Password);
            if (result.Succeeded)
            {
                _logger.Information($"User with id = {passwordDto.userId} is changed password successfully");

                return new Response
                {
                    HttpResponse = HttpResponse.Ok,
                    Message = "Update Password successfully ",
                    Data = user,
                    Succeeded = true
                };
            }

            return new Response
            {
                HttpResponse = HttpResponse.InternalServerErrror,
                Message = "an error occured during delete User",
                Data = result.Errors,
                Succeeded = result.Succeeded
            };

            
        }

        public async Task<Response> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new Response
                {
                    HttpResponse = HttpResponse.NotFound,
                    Message = $"there isn't users with Id= {userId}",
                };

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _logger.Information($"User with id = {userId} is Deleted successfully now");

                return new Response
                {
                    HttpResponse = HttpResponse.Ok,
                    Message = "deleted user successfully ",
                    Data = user,
                    Succeeded = true
                };
            }

            return new Response
            {
                HttpResponse = HttpResponse.InternalServerErrror,
                Message = "an error occured during delete User",
                Data = result.Errors,
                Succeeded = result.Succeeded
            };
        }

        public async Task<Response> GetUsersList()
        {
            var users =  _userManager.Users ;
            _logger.Information($"Get All Store Users");
            List<UserDto> userRolesList =new List<UserDto>();
            foreach (var user in users)
            {
                UserDto userDto = _mapper.Map<UserDto>(user);
                var userRoles = await _userManager.GetRolesAsync(user);
                userDto.userRoles = userRoles.FirstOrDefault();
                userRolesList.Add(userDto);
            }

            return new Response
            {
                HttpResponse = HttpResponse.Ok,
                Message = "returned all users successfully ",
                Data = users,
                Succeeded= true
            };

        }

        public async Task<Response> Login(LoginDto loginDto)
        {
            if (loginDto == null)
                throw new ArgumentNullException("loginDto object cannot be empty ");

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !user.Active)
                return new Response
                {
                    HttpResponse = HttpResponse.NotFound,
                    Message = $"invalid Email = {loginDto.Email} Or User not active please contact to support team",

                };
            _logger.Information($"User Email = {loginDto.Email} is valid and Active User");

            bool checkPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!checkPassword)
                return new Response
                {
                    HttpResponse = HttpResponse.NotFound,
                    Message = $"invalid Password",
                    Data = user

                };
            _logger.Information($"User Password is valid and User loggin Successfully Now");

            // ToDo Create New JWT Token
            var userRoles = await _userManager.GetRolesAsync(user);
            UserDto userDtoToken = _mapper.Map<UserDto>(user);
            userDtoToken.userRoles = userRoles.FirstOrDefault();
            userDtoToken.token = _jWTUtils.GenerateToken(userDtoToken);

            return new Response
            {
                HttpResponse = HttpResponse.Ok,
                Message = "returned user successfully ",
                Data = userDtoToken,
                Succeeded = true
            };
        }

        public async Task<Response> UpdateUser(EditUserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto));
            var user = await _userManager.FindByIdAsync(userDto.Id);
            if (user == null)
                return new Response
                {
                    HttpResponse = HttpResponse.NotFound,
                    Message = $"there isn't users with Id= {userDto.Id}",
                };
            _logger.Information($"User with id = {userDto.Id} is valid");

            var rolUsers = await _userManager.GetRolesAsync(user);
            var userRoles = rolUsers.FirstOrDefault();

            var MappeddUser = _mapper.Map<User>(userDto);

            var result = await _userManager.UpdateAsync(MappeddUser);
            if (result.Succeeded)
            {
                if(!await _userManager.IsInRoleAsync(user, userDto.roleName))
                {
                    await _userManager.RemoveFromRoleAsync(user, userRoles);
                   await _userManager.AddToRoleAsync(user, userDto.roleName);
                }
                

                    return new Response
                    {
                        HttpResponse = HttpResponse.Ok,
                        Message = "returned user successfully ",
                        Data = MappeddUser,
                        Succeeded = true
                    };
            }
                

            return new Response
            {
                HttpResponse = HttpResponse.InternalServerErrror,
                Message = "an error occured during updte user",
                Data = result.Errors,
                Succeeded = result.Succeeded
            };

        }
    }
}
