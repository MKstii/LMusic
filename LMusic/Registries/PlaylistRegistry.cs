using LMusic.Models;

namespace LMusic.Registries
{
    public class PlaylistRegistry : Registry<Playlist>
    {
        private Dictionary<UserAccess, Func<Playlist, bool>> _getAccess = new Dictionary<UserAccess, Func<Playlist, bool>>
        {
            [UserAccess.My] = (x) => { return true; },
            [UserAccess.Friend] = (x) => { return x.Privacy == Privacy.ForAll || x.Privacy == Privacy.ForFriends; },
            [UserAccess.User] = (x) => { return x.Privacy == Privacy.ForAll; }

        };
        public IEnumerable<Playlist> GetPlaylistsByUser(User user, UserAccess access) 
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                var playlists = db.Playlists.Where(x => x.UserId == user.Id).Where(_getAccess[access]).ToList();
                return playlists;
            }
        }
        public Playlist GetDefaultUserPlaylist(User user)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                var playlist = db.Playlists.Where(x => x.UserId == user.Id).Where(x => x.IsDefault == true).FirstOrDefault();
                return playlist;
            }
        }
    }
}
