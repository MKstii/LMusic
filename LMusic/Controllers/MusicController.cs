using LMusic.Models;
using LMusic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace LMusic.Controllers
{
    public class MusicController : Controller
    {
        private MusicService _musicService = new MusicService();
        private UserService _userService = new UserService();
        private PlaylistService _playlistService = new PlaylistService();
        private PictureService _pictureService = new PictureService();
        private FriendService _friendService = new FriendService();
        private AuthService _authService = new AuthService();

        // GET: MusicController
        public ActionResult Index()
        {
            Music music;
            using (var db = new ContextDataBase())
            {
                music = db.Musics.Where(x => x.User.Id == 5).FirstOrDefault();
            }
            return View(music);
        }

        // GET: MusicController/Edit/5
        [HttpGet("Give5")]
        public int Give5()
        {
            return 5;
        }

        [HttpPost("AddMusicToFav")]
        public IActionResult AddMusicToFav(int musicId)
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

                Music music = _musicService.GetMusic(musicId);
                switch (music.User.Privacy)
                {
                    case Privacy.ForAll:
                        if (_musicService.UserHasMusic(music, user))
                            return BadRequest("Музыка уже добавлена");
                        else
                            _musicService.AddMusicToUser(music, user);
                        return Redirect(Request.Headers["Referer"].ToString());
                    case Privacy.ForFriends:
                        if (_musicService.UserHasMusic(music, user))
                            return BadRequest("Музыка уже добавлена");
                        else if (_friendService.IsFriends(user, music.User))
                            _musicService.AddMusicToUser(music, user);
                        else
                            return BadRequest("Не удалось добавить музыку");
                        break;
                    case Privacy.ForMe:
                        return BadRequest("Не удалось добавить музыку");
                    default:
                        return BadRequest("Не удалось добавить музыку");
                }

                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                Response.Cookies.Delete("TelegramUserHash");
                return Redirect("/home");
            }

        }

        [HttpPost("AddMusicToPlaylist")]
        public IActionResult AddMusicToPlaylist(int musicId, int playlistId)
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
                if (_userService.GetAccess(playlistOwner, user) == UserAccess.My)
                {
                    Playlist playlist = _playlistService.GetPlaylistById(playlistId, UserAccess.My);
                    Music music = _musicService.GetMusic(musicId);
                    switch (music.User.Privacy)
                    {
                        case Privacy.ForAll:
                            if (_musicService.UserHasMusic(music, user))
                                return BadRequest("Музыка уже добавлена");
                            else
                                _musicService.AddMusicToPlaylist(music, playlist);
                            return Redirect(Request.Headers["Referer"].ToString());
                        case Privacy.ForFriends:
                            if (_musicService.PlaylistHasMusic(music, playlist))
                                return BadRequest("Музыка уже добавлена");
                            else if (_friendService.IsFriends(user, music.User))
                                _musicService.AddMusicToPlaylist(music, playlist);
                            else
                                return BadRequest("Не удалось добавить музыку");
                            break;
                        case Privacy.ForMe:
                            return BadRequest("Не удалось добавить музыку");
                        default:
                            return BadRequest("Не удалось добавить музыку");
                    }
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
