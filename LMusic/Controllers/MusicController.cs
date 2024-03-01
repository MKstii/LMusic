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
            return View();
        }

        // GET: MusicController/Edit/5
        [HttpGet("Give5")]
        public int Give5()
        {
            return 5;
        }

    }
}
