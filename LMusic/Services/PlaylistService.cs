using LMusic.Models;
using LMusic.Registries;
using LMusic.ViewModels;
using System.Security.Cryptography.X509Certificates;

namespace LMusic.Services
{
    public class PlaylistService : DbServiceAbstract<Playlist>
    {
        PlaylistRegistry _playlistRegistry;
        PlaylistMusicRegistry _playlistMusicRegistry;
        PictureService _pictureService;
        public PlaylistService() : base(new PlaylistRegistry())
        {
            _playlistRegistry = (PlaylistRegistry)_registry;
            _playlistMusicRegistry = new PlaylistMusicRegistry();
            _pictureService = new PictureService();
        }

        public IEnumerable<Playlist> GetPlaylistsByUser(User user, UserAccess access)
        {
            if(user.Privacy == Privacy.ForMe) return new List<Playlist>();
            if(user.Privacy == Privacy.ForFriends && access == UserAccess.User) return new List<Playlist>();
            return _playlistRegistry.GetPlaylistsByUser(user, access);
        }

        public Playlist CreateDefaultPlaylistByUser(User user)
        {
            var newPlaylist = new Playlist(user);
            _playlistRegistry.Add(newPlaylist);
            return newPlaylist;
        }

        public PlaylistViewmodel GetViewModel(Playlist playlist)
        {
            var viewmodel = new PlaylistViewmodel();
            viewmodel.Id = playlist.Id;
            viewmodel.Name = playlist.Name;
            viewmodel.PhotoPath = _pictureService.GetPlaylistAvatar(playlist).GetFullPath();
            return viewmodel;
        }

        public Playlist GetDefaultUserPlaylist(User user)
        {
            return _playlistRegistry.GetDefaultUserPlaylist(user);
        }
    }

    public static class PlaylistExtension
    {
        //public static IEnumerable<PlaylistViewmodel> ToViewModel(this IEnumerable<Playlist> playlists) 
        //{
        //    var viewmodels = playlists.Select(x => )
        //    return playlists.<Method name > (GetViewModel);
        //}
    }
}
