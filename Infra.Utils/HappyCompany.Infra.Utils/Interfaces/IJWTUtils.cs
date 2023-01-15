using HappyCompany.Domain.Dtos;

namespace HappyCompany.Infra.Utils.Interfaces
{
    public interface IJWTUtils
    {
        string GenerateToken(UserDto user);
    }
}
