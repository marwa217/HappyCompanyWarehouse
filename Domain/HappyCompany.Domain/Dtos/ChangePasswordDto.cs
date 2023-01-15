

namespace HappyCompany.Domain.Dtos
{
    public class ChangePasswordDto
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string? oldPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage =
        "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string userId { get; set; }
    }
}
