using LMusic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace LMusic.Controllers
{
    public class MusicController : Controller
    {
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

    }
}
