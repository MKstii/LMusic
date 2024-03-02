using LMusic.Models;
using LMusic.Registries;

namespace LMusic.Additional
{
    public class DatabaseData
    {
        private UserRegistry userRegistry = new UserRegistry();
        private PictureRegistry picRegistry = new PictureRegistry();
        private FriendRequestRegistry friendRequestRegistry = new FriendRequestRegistry();
        private FriendsListRegistry friendsListRegistry = new FriendsListRegistry();
        private MusicRegistry musicRegistry = new MusicRegistry();
        private PlaylistRegistry playlistRegistry = new PlaylistRegistry();

        public void Init()
        {
            picRegistry.Add(Pictures);
            userRegistry.Add(Users);
            friendRequestRegistry.Add(FriendRequests);
            friendsListRegistry.Add(FriendsLists);
            musicRegistry.Add(Musics);
            playlistRegistry.Add(Playlists);
        }

        public List<Picture> Pictures { get; } = new List<Picture>()
        {
            //дефолтные
            new Picture(1, "avatarDefault.jpg", PictureType.Avatar, true),
            new Picture(2, "musicDefault.png", PictureType.Music, true),
            new Picture(3, "playlistDefault.png", PictureType.Playlist, true),

            //аватарки
            new Picture(4, "JJgF7k7Znbw.jpg", PictureType.Avatar, false), //миша
            new Picture(5, "nTe9Z-ssd_c.jpg", PictureType.Avatar, false), //валя
            new Picture(6, "avatarDefault.jpg", PictureType.Avatar, false), //маша

            //Музыка
            new Picture(7, "e2be07c304a7472ca440a73b11a78cb7_464_464.jpg", PictureType.Music, false), //ANNA ASTI - Царица.mp3
            new Picture(8, "cb18c81a2dad1f8cc623968bb0fe05c812_resize_2000x2000_same_d504fb.png", PictureType.Music, false), //МакSим - Знаешь ли ты.mp3
            new Picture(9, "nervy_nervy.280.jpg", PictureType.Music, false), //Нервы - Нервы.mp3
        };
        public List<User> Users { get; } = new List<User>()
        {
            new User(1, "MKs", "213939128", "MKs", Privacy.ForAll, 40, 4),
            new User(2, "Helby", "916209678", "Helby", Privacy.ForAll, 39, 5),
            new User(3, "Masha", "172549090", "Masha", Privacy.ForAll, 45, 6),
            new User(4, "vlai1a", "730925826", "vlai1a", Privacy.ForAll, 45, 1),
        };

        public List<FriendRequest> FriendRequests { get; } = new List<FriendRequest>()
        {
            new FriendRequest(1, 1, 3),
            new FriendRequest(2, 2, 4)
        };

        public List<FriendsList> FriendsLists { get; } = new List<FriendsList>()
        {
            new FriendsList(1, 1, 4),
            new FriendsList(2, 4, 1),
            new FriendsList(3, 1, 2),
            new FriendsList(4, 2, 1),
            new FriendsList(5, 3, 4),
            new FriendsList(6, 4, 3)
        };

        public List<Music> Musics { get; } = new List<Music>()
        {
            new Music(1, 4, 7, "Царица", "ANNA ASTI", "ANNAASTIЦарица.mp3", Privacy.ForAll),
            new Music(2, 4, 8, "Знаешь ли ты", "МакSим", "МакSимЗнаешьлиты.mp3", Privacy.ForAll),
            new Music(3, 4, 9, "Нервы", "Нервы", "НервыНервы.mp3", Privacy.ForAll),
        };
        public List<Playlist> Playlists { get; } = new List<Playlist>()
        {
            new Playlist(1, "Бомба", 4, 3, Privacy.ForAll),
            new Playlist(2, "Песни", 1, 3, Privacy.ForAll)
        };
        public List<PlaylistMusic> PlaylistMusicArray { get; } = new List<PlaylistMusic>()
        {

        };
        

        
    }
}
