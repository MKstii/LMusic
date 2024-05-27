﻿using LMusic.Models;
using LMusic.Models.Requests;
using LMusic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LMusic.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService =  new UserService();
        private PlaylistService _playlistService = new PlaylistService();
        private PictureService _pictureService = new PictureService();
        private MusicService _musicService = new MusicService();
        private AuthService _authService = new AuthService();
        private IWebHostEnvironment _appEnvironment;

        public UserController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        [Route("/user/{id}")]
        public IActionResult Index(string id)
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            if (_authService.ValidUser(tgUser))
            {
                var user = _userService.GetUserByTg(tgUser);
                if(user == null)
                {
                    return Redirect("/home");
                }
                var userViewModel = _userService.GetUserViewModel(id, user);
                if(userViewModel == null)
                {
                    return View("NotFound");
                }
                return View(userViewModel);
            }
            else
            {
                Response.Cookies.Delete("TelegramUserHash");
                return Redirect("/home");
            }
            
        }

        public IActionResult Index()
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            if(tgUser == null)
            {
                return Redirect("/Home");
            }
            return Index(tgUser.id.ToString());
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
        public IActionResult ChangeAvatar(int userId, IFormFile pictureFile)
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            if (_authService.ValidUser(tgUser))
            {
                if (pictureFile.ContentType.Contains("image") && pictureFile != null)
                {
                    var user = _userService.GetUserByTg(tgUser);
                    var pic = _pictureService.CreatePicture(user, pictureFile, PictureType.Avatar, _appEnvironment.WebRootPath);
                    user.PictureId = pic.Id;

                    _userService.Update(user);
                    return Ok();
                }
                return BadRequest();
            }
            else
            {
                return Unauthorized("Ошибка проверки пользователя");
            }

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
                var playlists = _playlistService.GetPlaylistsByUser(user, UserAccess.My);
                return Ok(playlists);
            }
            
        }

        // ДОДЕЛАТЬ.
        // ЧТО ДОДЕЛАТЬ ТО??!?!?!?!? ВСЕ РАБОТАЕТ
        // Ниче не работает. При добавлении добавляй в стандартный плейлист
        [HttpPost]
        public IActionResult AddMusic(string title,string musician, IFormFile audioFile, IFormFile? musicPicture)
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            if (_authService.ValidUser(tgUser))
            {
                if (audioFile != null && audioFile.ContentType.Contains("audio"))
                {
                    var user = _userService.GetUserByTg(tgUser);
                    var music = _musicService.CreateMusic(user, title, musician, audioFile, musicPicture, _appEnvironment.WebRootPath);
                    return Redirect("/user");
                }
                return BadRequest();
            }
            else
            {
                return Unauthorized("Ошибка проверки пользователя");
            }
        }

        [HttpGet("GetUserMusic")]
        public IActionResult GetUserMusic(int userId)
        {
            using (var db = new ContextDataBase())
            {
                return Ok(db.Users.Where(x => x.Id == userId).Include(x => x.MusicArray).FirstOrDefault());
            }
            //    var user = _userService.Find(userId);
            //if (user == null || user.Privacy == Privacy.ForMe)
            //{
            //    return Forbid();
            //}
            //else
            //{
            //    var playlists = _playlistService.GetPlaylistsByUserId(userId);
            //    return Ok(playlists);
            //}

        }
    }
}
