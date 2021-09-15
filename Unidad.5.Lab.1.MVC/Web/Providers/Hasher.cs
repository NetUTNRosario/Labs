using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Web.Providers
{
    public interface IHasher
    {
        string GenerateHash(string input, string salt);
        string GenerateSalt(int size = 32);
    }

    public class Hasher : IHasher
    {
        public string GenerateHash(string input, string salt)
        {
            using var alg = new HMACSHA256(GetBytes(salt));
            var result = alg.ComputeHash(GetBytes(input));

            return Convert.ToBase64String(result);
        }

        public string GenerateSalt(int size = 32)
        {
            byte[] salt = new byte[size];
            using var rng = new RNGCryptoServiceProvider();
            // Esto llena la secuencia de bytes
            rng.GetNonZeroBytes(salt);

            return Convert.ToBase64String(salt);
        }

        private byte[] GetBytes(string str) => Encoding.UTF8.GetBytes(str);
    }
}
