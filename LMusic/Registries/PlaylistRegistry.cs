using LMusic.Models;

namespace LMusic.Registries
{
    public class PlaylistRegistry : Registry<Playlist>
    {
        public IEnumerable<Playlist> GetPlaylistsByUserId(int userId) 
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                var playlists = db.Playlists.Where(x => x.UserId == userId).ToList();
                return playlists;
            }
        }
    }
}
