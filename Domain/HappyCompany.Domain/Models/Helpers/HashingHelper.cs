using System.Security.Cryptography;
using System.Text;

namespace HappyCompany.Domain.Models.Helpers
{
    public static class HashingHelper
    {
        const int keySize = 64;
        static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        const int iterations = 350000;

        public static string HashPasword(this string value, out byte[] salt)
        {
         
            salt = RandomNumberGenerator.GetBytes(keySize);
            
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(value),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }

    }
}