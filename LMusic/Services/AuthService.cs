using LMusic.Models;
using LMusic.Models.Requests;
using LMusic.Registries;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace LMusic.Services
{
    public class AuthService : IService
    {
        public string BotToken = "7049955807:AAGn7hBC4HfL7e3cHtQeFvtuOhW9zR13un0";
        public UserRegistry UserReg = new UserRegistry();

        public AuthService() { }

        static string ByteToString(byte[] buff)
        {
            var result = Convert.ToHexString(buff).ToLower();
            return result;

        }
        public bool ValidUser(TelegrammUser user)
        {
            var hash = user.hash;
            string dataString = user.ToString();
            using SHA256 hash256 = SHA256.Create();
            var secret_key = hash256.ComputeHash(Encoding.UTF8.GetBytes(BotToken));
            string res_hash = "";
            using (HMACSHA256 hmachash256 = new HMACSHA256(secret_key))
            {
                res_hash = ByteToString(hmachash256.ComputeHash(Encoding.UTF8.GetBytes(dataString)));
            }
            if (String.Compare(res_hash, hash) != 0)
            {
                throw new MemberAccessException("Data Is Invalid!");
            }
            if(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - user.auth_date > 259200)
            {
                throw new DataException("Data is outdated");
            }
            return true;
        }        
    }

}
