using LMusic.Models.Requests;
using LMusic.Registries;
using LMusic.Services;

namespace LMusic.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TelegramId { get; set; }
        public string Login { get; set; }
        public Privacy Privacy { get; set; }
        public double FreeSpace { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
        public List<PlaylistUser> PlaylistArray { get; set; }
        public List<Music> MusicArray { get; set; }
        public List<FriendsList> FriendsList { get; set; }
        public List<FriendsList> FriendsListAsFriend { get; set; }
        public List<FriendRequest> FriendRequestsListAsAddressee { get; set; }
        public List<FriendRequest> FriendRequestsListAsRequester { get; set; }

        public User(TelegrammUser request)
        {
            TelegramId = request.id;
            Login = request.username;
            Name = request.first_name + " " + request.last_name;
            Privacy = Privacy.ForAll;
            FreeSpace = 50;
            PictureId = new PictureService().GetDefaulAvatarPicture().Id;
        } 
        public User() { }

        public User(int id, string name, int tgId, string login, Privacy privacy, double freeSpace, int pictureId)
        {
            Id = id;
            Name = name;
            TelegramId = tgId;
            Login = login;
            Privacy = privacy;
            FreeSpace = freeSpace;
            PictureId = pictureId;
        }
        public int GetId()
        {
            return Id;
        }
        private void Update()
        {
            var userRegister = new UserRegistry();
            userRegister.Update(this);
        }

        public void AddMusic(Music music)
        {
            MusicArray.Add(music);
            Update();
        }
    }
}
