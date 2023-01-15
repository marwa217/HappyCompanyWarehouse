

namespace HappyCompany.Domain.Dtos
{
    public class RegisterDto
    {
        public string fullname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RoleName { get; set; }
    }
}
