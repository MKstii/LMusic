using LMusic.Models;
using LMusic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace LMusic.Controllers
{
    public class MusicController : Controller
    {
        MusicService musicService = new MusicService();

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


            return Ok();
        }


    }
}
