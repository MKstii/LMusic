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

       

    }
}
