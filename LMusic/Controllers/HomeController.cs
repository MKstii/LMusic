using LMusic.Models;
using LMusic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace LMusic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserService _userService = new UserService();
        private AuthService _authService = new AuthService();


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            ViewData["IsAuthtorized"] = _authService.ValidUser(tgUser);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("/home/profile/{my}/{friend}/{user}")]
        public IActionResult Profile(string my, string friend, string user)
        {
            ViewData["myprofile"] = int.Parse(my) == 0 ? false : true;
            ViewData["friendprofile"] = int.Parse(friend) == 0 ? false : true;
            ViewData["userprofile"] = int.Parse(user) == 0 ? false : true;
            return View();
        }

        public IActionResult Music()
        {
            return View();
        }

        public IActionResult Friends()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
