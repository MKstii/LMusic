using LMusic.Models;
using LMusic.Models.Requests;
using LMusic.Registries;
using LMusic.ViewModels.User;
using System.Net;
using System.Text.Json;

namespace LMusic.Services
{
    public class UserService : DbServiceAbstract<User>
    {
        private UserRegistry _userRegisty;
        private PictureService _pictureService;
        private PlaylistService _playlistService;
        private MusicService _musicService;
        private FriendService _friendService;

        public UserService() : base(new UserRegistry())
        {
            _userRegisty = (UserRegistry)_registry;
            _pictureService = new PictureService();
            _playlistService = new PlaylistService();
            _musicService = new MusicService();
            _friendService = new FriendService();
        }

        public List<User> GetUsersByIds(int[] ids)
        {
            return _userRegisty.GetUsersByIds(ids);
        }

        public (List<User>, int) GetUsers(string filter = "", int page = 1, int limit = 20)
        {
            return _userRegisty.GetUsers(filter, page, limit);
        }

        public (List<User>, int) GetFriends(User user, string filter = "", int page = 1, int limit = 20)
        {
            return _userRegisty.GetFriends(user, filter, page, limit);
        }

        public User CreateUserByTg(TelegrammUser tgUser)
        {
            var user = new User(tgUser);
            _userRegisty.Add(user);
            _playlistService.CreateDefaultPlaylistByUser(user);

            return user;
        }

        public User? GetUserByTg(TelegrammUser? tgUser)
        {
            if (tgUser == null) return null;
            var user = _userRegisty.GetUserByTgId(tgUser.id.ToString());
            return user;
        }

        public User? GetUserByTg(string tgUserJson)
        {
            var tgUser = ConvertJsonToTgUser(tgUserJson);
            return GetUserByTg(tgUser);
        }

        public User? GetUserByTgId(string tgId)
        {
            return _userRegisty.GetUserByTgId(tgId);
        }

        public TelegrammUser? ConvertJsonToTgUser(string? tgUserJson)
        {
            if (tgUserJson == null || tgUserJson == "") return null;
            var tgUser = JsonSerializer.Deserialize<TelegrammUser>(tgUserJson);
            return tgUser;
        }

        public UserProfileViewModel? GetUserViewModel(string userTgId, User requestSender)
        {
            User? user = _userRegisty.GetUserByTgId(userTgId);

            if (user == null)
            {
                return null;
            }

            UserAccess access = GetAccess(user, requestSender);
            var viewmodel = new UserProfileViewModel();

            if(access == UserAccess.My)
            {
                user = requestSender;
                viewmodel.FreeSpace = user.FreeSpace;
            }

            viewmodel.UserProfileAccess = access;
            viewmodel.UserName = user.UserName;
            viewmodel.PhotoPath = _pictureService.GetUserAvatar(user).GetFullPath();
            viewmodel.TgId = user.TelegramId;

            if(user.Privacy == Privacy.ForAll
                || user.Privacy == Privacy.ForFriends && _friendService.IsFriends(user, requestSender)
                || user.Id == requestSender.Id)
            {
                viewmodel.Playlists = _playlistService.GetPlaylistsByUser(user, access).Select(x => _playlistService.GetViewModel(x, requestSender)).ToList();
                viewmodel.FavoriteMusic = _musicService.GetFavoriteMusicByUser(user, access).Select(x => _musicService.GetViewModel(x, requestSender)).ToList();
            }
            else
            {
                viewmodel.Playlists = new List<PlaylistViewmodel>();
                viewmodel.FavoriteMusic = new List<MusicViewmodel>();
            }

            return viewmodel;
            
        }
        public UserAccess GetAccess(User user, User requester)
        {
            if (user.Id == requester.Id)
            {
                return UserAccess.My;
            }
            else if (_friendService.IsFriends(user, requester))
            {
                return UserAccess.Friend;
            }
            else
            {
               return UserAccess.User;
            }
        }
    }
}
