using LMusic.Models;
using LMusic.Models.Requests;
using LMusic.Registries;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace LMusic.Services
{
    public class AuthService : IService, IDisposable
    {
        public string BotToken = "7049955807:AAGn7hBC4HfL7e3cHtQeFvtuOhW9zR13un0";
        public UserRegistry UserReg = new UserRegistry();

        public AuthService() { }

        static string ByteToString(byte[] buff)
        {
            //string sbinary = "";
            //for (int i = 0; i < buff.Length; i++)
            //    sbinary += buff[i].ToString("x2"); // hex format
            //return sbinary;
            var result = Convert.ToHexString(buff).ToLower();
                //BitConverter.ToString(buff).Replace("-", string.Empty).ToLower();
            return result;

        }
        private TelegrammUser AuthUser(TelegrammUser user)
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
  //          if (long.Parse(DateTimeOffset.UtcNow.ToString()) - long.Parse(user.auth_date) > 259200)
  //          {
  //              throw new DataException("Data is outdated");
  //          }
            return user;
        }

        public void Dispose() {}

        public TelegrammUser LoginUser(TelegrammUser user) 
        {
            if (UserReg.FindTelgrammId(user.id) == null)
            {
                AuthUser(user);
                User newUser = new User(user);
                UserReg.Add(newUser);
            }
            else
            {
                AuthUser(user);
            }
            return user;
        }
        
    }

}
