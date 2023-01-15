

using HappyCompany.Domain.Interface.Common;

namespace HappyCompany.Domain.Models.Helpers
{
    public class JwtSettings : IJwtSettings
    {
        public string Site { get { return "http://www.security.org"; } }
        public string SigningKey { get { return "officials NewYork City Pinterest but considering"; } }
        public string ExpiryInMinutes { get { return "2592000"; } }
    }
}
