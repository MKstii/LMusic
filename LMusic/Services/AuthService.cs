using LMusic.Models;
using LMusic.Models.Requests;
using LMusic.Registries;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;

namespace LMusic.Services
{
    public class AuthService : IService
    {
        private string BotToken = "7049955807:AAGn7hBC4HfL7e3cHtQeFvtuOhW9zR13un0";

        public AuthService() { }

        static string ByteToString(byte[] buff)
        {
            var result = Convert.ToHexString(buff).ToLower();
            return result;

        }
        public bool ValidUser(TelegrammUser? tgUser)
        {
            if(tgUser == null) return false;
            var hash = tgUser.hash;
            string dataString = tgUser.ToString();
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
            if(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - tgUser.auth_date > 259200)
            {
                throw new DataException("Data is outdated");
            }
            return true;
        }

        //public User? ValidUser(string userJson, bool createIfNull)
        //{
        //    if(userJson == "" || userJson == null) return null;
        //    var user = JsonSerializer.Deserialize<TelegrammUser>(userJson);
        //    return ValidUser(user, createIfNull);
        //}

        public TelegrammUser? ConvertJson(string userJson)
        {
            return JsonSerializer.Deserialize<TelegrammUser>(userJson);
        }

        //public void ClearCookie(HttpRequest request, string cookieName)
        //{
        //    HttpCookie myCookie = new HttpCookie(cookieName);
        //    myCookie.Expires = DateTime.Now.AddDays(-1d);
        //    request.Cookies.Add(myCookie);
        //}
    }

}
