

namespace HappyCompany.Domain.Interface.Common
{
    public interface IJwtSettings
    {
        public string Site { get; }
        public string SigningKey { get; }
        public string ExpiryInMinutes { get; }
    }
}
