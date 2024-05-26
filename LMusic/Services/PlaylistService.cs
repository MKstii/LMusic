using LMusic.Models;
using LMusic.Registries;
using LMusic.ViewModels.User;
using System.Security.Cryptography.X509Certificates;

namespace LMusic.Services
{
    public class PlaylistService : DbServiceAbstract<Playlist>
    {
        PlaylistRegistry _playlistRegistry;
        PlaylistMusicRegistry _playlistMusicRegistry;
        PictureService _pictureService;
        PlaylistUserRegistry _playlistUserRegistry;
        public PlaylistService() : base(new PlaylistRegistry())
        {
            _playlistRegistry = (PlaylistRegistry)_registry;
            _playlistMusicRegistry = new PlaylistMusicRegistry();
            _pictureService = new PictureService();
            _playlistUserRegistry = new PlaylistUserRegistry();
        }

        public IEnumerable<Playlist> GetPlaylistsByUser(User user, UserAccess access)
        {
            if(user.Privacy == Privacy.ForMe) return new List<Playlist>();
            if(user.Privacy == Privacy.ForFriends && access == UserAccess.User) return new List<Playlist>();
            var playlistsUsers = _playlistUserRegistry.GetByUser(user);
            var playlistsUsersIds = playlistsUsers.Select(x => x.Id).ToList();
            return _playlistRegistry.GetPlaylists(playlistsUsersIds, access);
        }

        public Playlist CreateDefaultPlaylistByUser(User user)
        {
            var DefaultPict = _pictureService.GetDefaulPlaylistPicture();
            var newPlaylist = Playlist.CreateDefault(DefaultPict);
            _playlistRegistry.Add(newPlaylist);
            var playlistUser = new PlaylistUser() {
                UserId = user.Id,
                PlaylistId = newPlaylist.Id,
                IsCreater = true,
                isDefault = true
            };
            _playlistUserRegistry.Add(playlistUser);

            return newPlaylist;
        }

        public PlaylistViewmodel GetViewModel(Playlist playlist)
        {
            var viewmodel = new PlaylistViewmodel();
            viewmodel.Id = playlist.Id;
            viewmodel.Name = playlist.Name;
            viewmodel.PhotoPath = _pictureService.GetPlaylistAvatar(playlist).GetFullPath();
            viewmodel.IsDefault = playlist.IsDefault;
            return viewmodel;
        }
        

        public Playlist GetDefaultUserPlaylist(User user)
        {
            var userPlaylist = _playlistUserRegistry.GetByUserCreater(user);
            return _playlistRegistry.Find(userPlaylist.PlaylistId);
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
