using System.Security.Cryptography;
using System.Text;

namespace Api.Auth
{
    public class CryptService
    {

        public CryptService() { }

        public string Encrypt(string password)
        {

            var salt = Encoding.UTF8.GetBytes(Configuration.Salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            var hash = pbkdf2.GetBytes(20);

            string savedPasswordHash = Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);

            return savedPasswordHash;
        }
    }
}
