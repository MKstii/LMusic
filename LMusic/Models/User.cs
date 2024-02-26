namespace LMusic.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public PrivateSettings Settings { get; set; }
        public double FreeSpace { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
        public List<Playlist> PlaylistArray { get; set; }
        public List<Music> MusicArray { get; set; }
        public List<FriendsList> FriendsList { get; set; }
        public List<FriendRequest> FriendRequestsList { get; set; }

    }
}
