

namespace HappyCompany.Domain.Dtos
{
    public class UserDto : User
    {
        public string token { get; set; }
        public string userRoles { get; set; }
        
    }
}
