using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;

namespace LMusic.Models.Requests
{
    public class TelegrammUser 
    {
        public int id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? username { get; set; }
        public string? photo_url { get; set; }
        public long auth_date { get; set; }
        public string hash { get; set; }

        public override string ToString()
        {
            var list = GetList();
            list.Sort();
            return String.Join("\n", list); 
        }

        public List<string> GetList()
        {
            var res = new List<string>
            {
                $"id={id}",
                $"auth_date={auth_date}"
            };

            res.AddIfNotNull("first_name",first_name);
            res.AddIfNotNull("last_name", last_name);
            res.AddIfNotNull("username", username);
            res.AddIfNotNull("photo_url", photo_url);

            return res;
        }


    }

    public static class ListExtension
    {
        public static void AddIfNotNull(this List<string> list, string name, object anyField)
        {
            if (anyField != null)
            {
                string str = $"{name}={anyField}";
                list.Add(str);
            }
        }
    }

}
