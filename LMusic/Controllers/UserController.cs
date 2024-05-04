using LMusic.Models;
using LMusic.Models.Requests;
using LMusic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMusic.Controllers
{
    
    public class UserController : Controller
    {
        private UserService _userService =  new UserService();
        private PlaylistService _playlistService = new PlaylistService();
        private PictureService _pictureService = new PictureService();
        private MusicService _musicService = new MusicService();
        private IWebHostEnvironment _appEnvironment;

        public UserController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            // ИЗМЕНИТЬ НА СЕРВИС + ФИЛЬТРЫ
            using (ContextDataBase db = new ContextDataBase())
            {
                return db.Users.ToArray();
            }
        }
        public User GetUser(int id)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                return db.Users.Where(x => x.Id == id).Include(x => x.Picture).FirstOrDefault();
            }
        }

        [HttpPost("ChangeAvatar")]
        public async Task<IActionResult> ChangeAvatar(int userId, IFormFile file)
        {
            if (file.ContentType.Contains("image") && file != null)
            {
                var pic = new Picture();
                pic.FileName = file.FileName;
                pic.Type = PictureType.Avatar;
                pic.IsDefault = false;
                pic.IsDeleted = false;
                var user = _userService.Find(userId);
                pic.Path = _pictureService.CreatePath(pic, user);
                Directory.CreateDirectory(_appEnvironment.WebRootPath + pic.Path);
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pic.GetFullPath(), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                _pictureService.Add(pic);
                user.PictureId = pic.Id;
                _userService.Update(user);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetUserPlaylists")]
        public IActionResult GetUserPlaylists(int userId)
        {
            var user = _userService.Find(userId);
            if(user == null || user.Privacy == Privacy.ForMe)
            {
                return Forbid();
            }
            else
            {
                var playlists = _playlistService.GetPlaylistsByUserId(userId);
                return Ok(playlists);
            }
            
        }

        [HttpPost("AddMusic")]
        public async Task<IActionResult> AddMusic(int userId, string title,string musician, IFormFile file)
        {
            //return Ok(file.ContentType);
            if (file.ContentType.Contains("audio") && file != null)
            {
                var music = new Music();
                music.FileName = file.FileName;
                music.IsDeleted = false;
                var user = _userService.Find(userId);
                music.Path = _musicService.CreatePath(user);
                music.UserId = user.Id;
                music.PictureId = _pictureService.GetDefaulMusicPicture().Id;
                music.Musician = musician;
                music.Title = title;

                Directory.CreateDirectory(_appEnvironment.WebRootPath + music.Path);
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + music.GetFullPath(), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                _musicService.Add(music);

                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetUserMusic")]
        public IActionResult GetUserMusic(int userId)
        {
            using (var db = new ContextDataBase())
            {
                return Ok(db.Users.Where(x => x.Id == userId).Include(x => x.MusicArray).FirstOrDefault());
            }
                var user = _userService.Find(userId);
            if (user == null || user.Privacy == Privacy.ForMe)
            {
                return Forbid();
            }
            else
            {
                var playlists = _playlistService.GetPlaylistsByUserId(userId);
                return Ok(playlists);
            }

        }
    }
}
