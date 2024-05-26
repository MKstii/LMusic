using LMusic.Models.Requests;
using LMusic.Registries;
using LMusic.Services;

namespace LMusic.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string TelegramId { get; set; }
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
            TelegramId = request.id.ToString();
            UserName = request.username;
            Privacy = Privacy.ForAll;
            FreeSpace = 50;
            PictureId = new PictureService().GetDefaulAvatarPicture().Id;
        } 
        public User() { }

        public User(int id, int tgId, Privacy privacy, double freeSpace, int pictureId)
        {
            Id = id;
            TelegramId = tgId.ToString();
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
