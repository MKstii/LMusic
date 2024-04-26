using System.Security.Cryptography.Xml;

namespace LMusic.Models.Requests
{
    public class TelegrammUser 
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string photo_url { get; set; }
        public string auth_date { get; set; }
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
                $"first_name={first_name}",
                $"last_name={last_name}",
                $"username={username}",
                $"photo_url={photo_url}",
                $"auth_date={auth_date}"
            };
            return res;
        }
    }
}
