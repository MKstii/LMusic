using LMusic.Models;
using LMusic.Registries;

namespace LMusic.Services
{
    public class PlaylistService : DbServiceAbstract<Playlist>
    {
        PlaylistRegistry _playlistRegistry;
        PlaylistMusicRegistry _playlistMusicRegistry;
        public PlaylistService() : base(new PlaylistRegistry())
        {
            _playlistRegistry = (PlaylistRegistry)_registry;
            _playlistMusicRegistry = new PlaylistMusicRegistry();
        }

        public IEnumerable<Playlist> GetPlaylistsByUserId(int userId)
        {
            return _playlistRegistry.GetPlaylistsByUserId(userId);
        }

    }
}
