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
        private PictureService _pictureService = new PictureService();
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
                    Response.Cookies.Delete("TelegramUserHash");
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

        [HttpPost("/playlist/addPlaylistToUser")]
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
                        else
                           _playlistService.AddPlaylistToUser(playlist, user);
                        return Redirect(Request.Headers["Referer"].ToString());
                    case Privacy.ForFriends:
                        if (_playlistService.UserHasPlaylist(playlist, user))
                            return BadRequest("Плейлист уже добавлен");
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

        [HttpPost("/playlist/add")]
        public IActionResult Add(string title, IFormFile? playlistPicture, Privacy privacy)
        {
            if(title == null)
            {
                return BadRequest("Укажите название");
            }

            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            if (_authService.ValidUser(tgUser))
            {
                var user = _userService.GetUserByTg(tgUser);

                if (user == null)
                {
                    Response.Cookies.Delete("TelegramUserHash");
                    return BadRequest("Пользователь не найден");
                }

                _playlistService.CreatePlaylist(user, title, playlistPicture, privacy, _appEnvironment.WebRootPath);

                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                Response.Cookies.Delete("TelegramUserHash");
                return Unauthorized("Ошибка валидации");
            }
        }

        [HttpPost("/playlist/delete")]
        public IActionResult Delete(int id)
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            if (_authService.ValidUser(tgUser))
            {
                var user = _userService.GetUserByTg(tgUser);

                if (user == null)
                {
                    Response.Cookies.Delete("TelegramUserHash");
                    return BadRequest("Пользователь не найден");
                }

                var owner = _playlistService.GetPlaylistOwner(id);

                if(owner.Id != user.Id)
                {
                    return BadRequest("Недостаточно прав");
                }

                var playlist = _playlistService.GetPlaylistById(id, UserAccess.My);

                if(playlist == null) 
                {
                    return BadRequest("Плейлист не найден");
                }

                _playlistService.Delete(playlist);

                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                Response.Cookies.Delete("TelegramUserHash");
                return Unauthorized("Ошибка валидации");
            }
        }

        [HttpPost("/playlist/update")]
        public IActionResult Update(int id, string title, IFormFile? playlistPicture, Privacy? privacy)
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            if (_authService.ValidUser(tgUser))
            {
                var user = _userService.GetUserByTg(tgUser);

                if (user == null)
                {
                    Response.Cookies.Delete("TelegramUserHash");
                    return BadRequest("Пользователь не найден");
                }

                var owner = _playlistService.GetPlaylistOwner(id);

                if (owner.Id != user.Id)
                {
                    return BadRequest("Недостаточно прав");
                }

                var playlist = _playlistService.GetPlaylistById(id, UserAccess.My);

                if (playlist == null)
                {
                    return BadRequest("Плейлист не найден");
                }

                playlist.Name = title == null ? playlist.Name : title;
                playlist.Privacy = privacy == null ? playlist.Privacy : (Privacy)privacy;

                if(playlistPicture != null)
                {
                    playlist.PictureId = _pictureService.CreatePicture(user, playlistPicture, PictureType.Playlist, _appEnvironment.WebRootPath).Id;
                }

                _playlistService.Update(playlist);

                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                Response.Cookies.Delete("TelegramUserHash");
                return Unauthorized("Ошибка валидации");
            }
        }

    }
}
