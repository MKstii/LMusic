using LMusic.Models.Requests;

namespace LMusic.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TelegramId { get; set; }
        public string Login { get; set; }
        public PrivateSettings Settings { get; set; }
        public double FreeSpace { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
        public List<Playlist> PlaylistArray { get; set; }
        public List<Music> MusicArray { get; set; }
        public List<FriendsList> FriendsList { get; set; }
        public List<FriendRequest> FriendRequestsList { get; set; }

        public User(TelegrammUser request)
        {
            TelegramId = request.Id;
            Login = request.Username;
            FreeSpace = 51200;
            PictureId = 1;
        } 
        public int GetId()
        {
            return Id;
        }
    }
}
