using LMusic.Models;
using LMusic.Models.Requests;
using LMusic.Registries;
using LMusic.ViewModels;
using System.Text.Json;

namespace LMusic.Services
{
    public class UserService : DbServiceAbstract<User>
    {
        private UserRegistry _userRegisty;
        private PictureService _pictureService;
        private PlaylistService _playlistService;
        private MusicService _musicService;

        public UserService() : base(new UserRegistry())
        {
            _userRegisty = (UserRegistry)_registry;
            _pictureService = new PictureService();
            _playlistService = new PlaylistService();
            _musicService = new MusicService();
        }

        public User CreateUserByTg(TelegrammUser tgUser)
        {
            var user = new User(tgUser);
            _userRegisty.Add(user);
            new PlaylistService().CreateDefaultPlaylistByUser(user);

            return user;
        }

        public User? GetUserByTg(TelegrammUser? tgUser)
        {
            if (tgUser == null) return null;
            var user = _userRegisty.GetUserByTgId(tgUser.id);
            return user;
        }

        public User? GetUserByTg(string tgUserJson)
        {
            var tgUser = ConvertJsonToTgUser(tgUserJson);
            return GetUserByTg(tgUser);
        }

        public TelegrammUser? ConvertJsonToTgUser(string? tgUserJson)
        {
            if (tgUserJson == null || tgUserJson == "") return null;
            var tgUser = JsonSerializer.Deserialize<TelegrammUser>(tgUserJson);
            return tgUser;
        }

        public UserProfileViewModel GetUserViewModel(int userTgId, User requestSender)
        {
            User user;
            UserAccess access;
            var viewmodel = new UserProfileViewModel();
            if (userTgId == requestSender.TelegramId)
            {
                user = requestSender;
                access = UserAccess.My;
                viewmodel.FreeSpace = user.FreeSpace;
            }
            // сделать проверку на друзей
            //else if ()
            else
            {
                access = UserAccess.User;
                user = _userRegisty.GetUserByTgId(userTgId);
            }
            
            viewmodel.UserProfileAccess = access;
            viewmodel.UserName = user.Name;
            viewmodel.PhotoPath = _pictureService.GetUserAvatar(user).GetFullPath();
            viewmodel.Playlists = _playlistService.GetPlaylistsByUser(user, access).Select(_playlistService.GetViewModel).ToList();
            viewmodel.FavoriteMusic = _musicService.GetFavoriteMusicByUser(user, access).Select(_musicService.GetViewModel).ToList();

            return viewmodel;
            
        }
    }
}
