using System.Security.Cryptography.Xml;

namespace LMusic.Models.Requests
{
    public class TelegrammUser 
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string photo_url { get; set; }
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
                $"first_name={first_name}",
                $"username={username}",
                $"photo_url={photo_url}",
                $"auth_date={auth_date}"
            };
            if (last_name == null) res.Add($"last_name=");
            else res.Add($"last_name={last_name}");

            if (photo_url == null) res.Add($"photo_url=");
            else res.Add($"photo_url={photo_url}");

            return res;
        }
    }
}
