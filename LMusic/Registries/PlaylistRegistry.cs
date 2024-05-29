using LMusic.Models;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<Playlist> GetPlaylists(List<int> playlistUserIds, UserAccess access)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                var playlists = db.Playlists.Where(x => playlistUserIds.Contains(x.Id)).Where(_getAccess[access]).ToList();
                return playlists;
            }
        }

        public List<Music> GetMusicsByPlaylist(Playlist playlist)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<PlaylistMusic> dbSet = db.Set<PlaylistMusic>();
                var musics = dbSet.Where(x => x.PlaylistId == playlist.Id).Include(x => x.Music).Select(x => x.Music).ToList();
                return musics;
            }
        }

        public Playlist? GetPlaylistById(int id, UserAccess access)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<Playlist> dbSet = db.Set<Playlist>();
                var musics = dbSet.Where(x => x.Id == id)
                    .Where(_getAccess[access])
                    .FirstOrDefault();
                return musics;
            }
        }
        //public Playlist GetDefaultUserPlaylist(User user)
        //{
        //    using (ContextDataBase db = new ContextDataBase())
        //    {
        //        var playlist = db.Playlists.Where(x => x.UserId == user.Id).Where(x => x.IsDefault == true).FirstOrDefault();
        //        return playlist;
        //    }
        //}


    }
}
