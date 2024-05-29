using LMusic.Models;
using LMusic.Services;
using LMusic.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMusic.Controllers
{
    public class PlaylistController : Controller
    {
        private AuthService _authService = new AuthService();
        private UserService _userService = new UserService();
        private PlaylistService _playlistService = new PlaylistService();
        private MusicService _musicService = new MusicService();
        private FriendService _friendService = new FriendService();
        private IWebHostEnvironment _appEnvironment;

        public PlaylistController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        [HttpGet("/playlist/{id}")]
        public IActionResult GetMusicListByPlaylist(int id)
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            if (_authService.ValidUser(tgUser))
            {
                var user = _userService.GetUserByTg(tgUser);
                var playlistOwner = _playlistService.GetPlaylistOwner(id);

                if (user == null || playlistOwner == null)
                {
                    return BadRequest("Пользователь не найден");
                }

                var access = _userService.GetAccess(playlistOwner, user);
                var playlist = _playlistService.GetPlaylistById(id, access);

                if (playlist == null)
                {
                    return BadRequest("Плейлист не найден или недостаточно прав");
                }

                var musics = _playlistService.GetMusicByPlaylist(playlist);

                var result = musics.Select(x => _musicService.GetViewModel(x));

                return Ok(result);
            }
            else
            {
                Response.Cookies.Delete("TelegramUserHash");
                return Unauthorized("Ошибка валидации");
            }
        }

        public IActionResult AddPlaylistToUser(int playlistId)
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            if (_authService.ValidUser(tgUser))
            {
                var user = _userService.GetUserByTg(tgUser);
                if (user == null)
                {
                    return Redirect("/home");
                }


                var playlistOwner = _playlistService.GetPlaylistOwner(playlistId);
                Playlist playlist = _playlistService.GetPlaylistById(playlistId, UserAccess.My);
                switch (playlistOwner.Privacy)
                {
                    case Privacy.ForAll:
                        if (_playlistService.UserHasPlaylist(playlist, user))
                            return BadRequest("Плейлист уже добавлен");
                        else if (playlist.IsDefault)
                            return BadRequest("Нелзья добавить плейлист \"Избранное\"");
                        else
                           _playlistService.AddPlaylistToUser(playlist, user);
                        return Redirect(Request.Headers["Referer"].ToString());
                    case Privacy.ForFriends:
                        if (_playlistService.UserHasPlaylist(playlist, user))
                            return BadRequest("Плейлист уже добавлен");
                        else if (playlist.IsDefault)
                            return BadRequest("Нелзья добавить плейлист \"Избранное\"");
                        else if (_friendService.IsFriends(user, playlistOwner))
                            _playlistService.AddPlaylistToUser(playlist, user);
                        else
                            return BadRequest("Не удалось добавить плейлист");
                        break;
                    case Privacy.ForMe:
                        return BadRequest("Не удалось добавить плейлист");
                    default:
                        return BadRequest("Не удалось добавить плейлист");
                }
                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                Response.Cookies.Delete("TelegramUserHash");
                return Redirect("/home");
            }
        }

    }
}
