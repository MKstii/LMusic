using System.Security.Cryptography.Xml;

namespace LMusic.Models.Requests
{
    public class TelegrammUser 
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        public string AuthDate { get; set; }
        public string Hash { get; set; }

        public override string ToString()
        {
            string res = "";
            var list = GetList();
            list.Sort();
            res = String.Join("\n", list);
            return res;
        }

        public List<string> GetList()
        {
            var res = new List<string>
            {
                $"id={Id}",
                $"first_name={FirstName}",
                $"last_name={LastName}",
                $"username={Username}",
                $"photo_url={PhotoUrl}",
                $"auth_date={AuthDate}",
            };
            return res;
        }
    }
}
