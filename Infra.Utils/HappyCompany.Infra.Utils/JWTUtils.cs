using HappyCompany.Domain.Dtos;
using HappyCompany.Domain.Interface.Common;
using HappyCompany.Infra.Utils.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HappyCompany.Infra.Utils
{
    public class JWTUtils : IJWTUtils
    {
        IJwtSettings _jwtSettings;

        public JWTUtils(IJwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(UserDto user)
        {
            var signinKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));

            int expiryInMinutes = Convert.ToInt32(_jwtSettings.ExpiryInMinutes);

            string site = _jwtSettings.Site;
            try
            {
                var token = new JwtSecurityToken(
                claims: new Claim[]
                {
                        new Claim(ClaimTypes.Name,user.FullName),
                        new Claim("Email",user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, user.userRoles),
                },
                issuer: site,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                audience: site,
                signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256));

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    
}
