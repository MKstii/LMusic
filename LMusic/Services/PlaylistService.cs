using LMusic.Models;
using LMusic.Registries;
using LMusic.ViewModels.User;
using Microsoft.AspNetCore.Http;
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
            var playlistsUsers = _playlistUserRegistry.GetByUser(user);
            var playlistsUsersIds = playlistsUsers.Select(x => x.PlaylistId).ToList();
            return _playlistRegistry.GetPlaylists(playlistsUsersIds, access);
        }

        public IEnumerable<Playlist> GetPlaylistsCreater(User user, UserAccess access)
        {
            var playlistsUsers = _playlistUserRegistry.GetByUserCreater(user);
            var playlistsUsersIds = playlistsUsers.Select(x => x.PlaylistId).ToList();
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

        public PlaylistViewmodel GetViewModel(Playlist playlist, User requester)
        {
            var viewmodel = new PlaylistViewmodel();
            viewmodel.Id = playlist.Id;
            viewmodel.Name = playlist.Name;
            viewmodel.PhotoPath = _pictureService.GetPlaylistAvatar(playlist).GetFullPath();
            viewmodel.IsDefault = playlist.IsDefault;
            var owner = GetPlaylistOwner(playlist.Id);
            viewmodel.CanEdit = owner.Id == requester.Id;
            viewmodel.IsAdded = UserHasPlaylist(playlist, requester);
            return viewmodel;
        }
        

        public Playlist? GetDefaultUserPlaylist(User user)
        {
            var userPlaylist = _playlistUserRegistry.GetByUserDefaultCreater(user);
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

        public bool UserHasPlaylist(Playlist playlist, User user)
        {
            var result = _playlistUserRegistry.GetByPlaylistAndUser(playlist, user);
            return result != null;
        }

        public void AddPlaylistToUser(Playlist playlist, User user)
        {
            PlaylistUser playlistUser = new PlaylistUser()
            {
                UserId = user.Id,
                PlaylistId = playlist.Id
            };
            _playlistUserRegistry.Add(playlistUser);
        }

        public Playlist CreatePlaylist(User user, string title, IFormFile? playlistPicture, Privacy priavcy, string webRootPath)
        {
            var playlist = new Playlist();
            Picture picture;
            if(playlistPicture == null)
            {
                picture = _pictureService.GetDefaulPlaylistPicture();
            }
            else
            {
                picture = _pictureService.CreatePicture(user, playlistPicture, PictureType.Playlist, webRootPath);
            }

            playlist.Name = title;
            playlist.PictureId = picture.Id;
            playlist.Privacy = priavcy;
            playlist.IsDefault = false;

            Add(playlist);

            var playlistUser = new PlaylistUser();
            playlistUser.isDefault = false;
            playlistUser.PlaylistId = playlist.Id;
            playlistUser.UserId = user.Id;
            playlistUser.IsCreater = true;

            _playlistUserRegistry.Add(playlistUser);

            return playlist;
        }

        public void RemovePlaylistFromUser(Playlist playlist, User user)
        {
            var playlistUser = _playlistUserRegistry.GetByPlaylistAndUser(playlist, user);
            _playlistUserRegistry.Delete(playlistUser);
        }

        public bool IsCreater(Playlist playlist, User user)
        {
            var result = _playlistUserRegistry.GetByPlaylistAndUser(playlist, user);
            if (result == null)
                return false;
            return result.IsCreater;
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
