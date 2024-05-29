using LMusic.Models;
using LMusic.Registries;
using LMusic.ViewModels.User;
using System.Security.Cryptography.X509Certificates;

namespace LMusic.Services
{
    public class PlaylistService : DbServiceAbstract<Playlist>
    {
        private PlaylistRegistry _playlistRegistry;
        private PlaylistMusicRegistry _playlistMusicRegistry;
        private PictureService _pictureService;
        private PlaylistUserRegistry _playlistUserRegistry;
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
        

        public Playlist? GetDefaultUserPlaylist(User user)
        {
            var userPlaylist = _playlistUserRegistry.GetByUserCreater(user);
            return _playlistRegistry.Find(userPlaylist.PlaylistId);
        }

        public List<Music> GetMusicByPlaylist(Playlist playlist)
        {
            return _playlistRegistry.GetMusicsByPlaylist(playlist);
        }

        public Playlist? GetPlaylistById(int id, UserAccess access)
        {
            var userPlaylistOwner = _playlistUserRegistry.GetUserOwnerByPlaylistId(id);

            if(userPlaylistOwner == null)
            {
                return null;
            }

            //UserAccess access = _userService.GetAccess(userPlaylistOwner, requester);
            var playlist = _playlistRegistry.GetPlaylistById(id, access);
            return playlist;
        }

        public User? GetPlaylistOwner(int playlistId)
        {
            return _playlistUserRegistry.GetUserOwnerByPlaylistId(playlistId);
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
