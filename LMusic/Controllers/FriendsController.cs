using LMusic.Models;
using LMusic.Services;
using LMusic.ViewModels.Friends;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMusic.Controllers
{
    public class FriendsController : Controller
    {
        private UserService _userService = new UserService();
        private AuthService _authService = new AuthService();
        private FriendService _friendService = new FriendService();

        [HttpGet]
        [Route("/friends")]
        public IActionResult Index(string filter = "", int page = 1, int limit = 20)
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

                var friends = _userService.GetFriends(user, filter, page, limit);

                var viewmodel = new FriendsSearchPageViewModel();
                viewmodel.Users = friends.Select(x => _friendService.GetUserViewmode_FriendsPage(x)).ToList();

                return View(viewmodel);
            }
            else
            {
                return Redirect("/home");
            }
        }

        [HttpGet]
        [Route("/friends/search")]
        public IActionResult Search(string filter = "", int page = 1, int limit = 20)
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

                var (friends, total) = _userService.GetUsers(filter, page, limit);
                var maxPage = total % limit == 0 ? total / limit : total / limit + 1;

                var viewmodel = new FriendsSearchPageViewModel();
                viewmodel.Users = friends.Select(x => _friendService.GetUserViewmode_FriendsPage(x)).ToList();
                viewmodel.Page = page;
                viewmodel.Limit = limit;
                viewmodel.MaxPage = maxPage;

                return View(viewmodel);
            }
            else
            {
                return Redirect("/home");
            }
        }

        [HttpGet]
        [Route("/friends/requests")]
        public IActionResult Requests(string filter = "", int page = 1, int limit = 20)
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

                var requests = _friendService.GetRequestByAddresser(user, filter, page, limit);
                var usersViewmodel = requests.Select(x => _friendService.GetUserViewmode_FriendsPage(x.Requester)).ToList();
                ViewData["filter"] = filter;
                return View(usersViewmodel);
            }
            else
            {
                return Redirect("/home");
            }
        }

        [HttpPost("sendFriendRequest")]
        public IActionResult SendFriendRequest(string addresseTgId)
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            if (_authService.ValidUser(tgUser))
            {
                var requester = _userService.GetUserByTg(tgUser);
                var addressee = _userService.GetUserByTgId(addresseTgId);

                // Проверка на существование пользователей
                if (requester == null || addressee == null)
                {
                    return BadRequest("Пользователь не найден");
                }

                // Проверка на обратную заявку.
                if(_friendService.HasRequest(addressee, requester))
                {
                    // Принять заявку
                }

                // Проверка на ту же заявку
                if (_friendService.HasRequest(requester, addressee))
                {
                    return BadRequest("Заявка уже существует");
                }

                _friendService.AddRequest(requester, addressee);

                Response.StatusCode = 200;
                return Redirect($"/user/{addresseTgId}");
            }
            else
            {
                return Unauthorized("Ошибка проверки пользователя");
            }
        }

        // Сделать
        [HttpPost("acceptFriendRequest")]
        public IActionResult AcceptFriendRequest(string tgIdRequester)
        {
            var acceptAction = (FriendRequest request) =>
            {
                _friendService.AcceptRequest(request);
                _friendService.DeleteRequest(request);
            };
            return AnswerActionOnRequsest(tgIdRequester, acceptAction);
        }

        [HttpPost("denyFriendRequest")]
        public IActionResult DenyFriendRequest(string tgIdRequester)
        {
            var denyAction = _friendService.DenyRequest;
            return AnswerActionOnRequsest(tgIdRequester, denyAction);
        }

        private IActionResult AnswerActionOnRequsest(string tgIdRequester, Action<FriendRequest> work)
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            if (_authService.ValidUser(tgUser))
            {
                var addressee = _userService.GetUserByTg(tgUser);
                var requester = _userService.GetUserByTgId(tgIdRequester);

                if (addressee == null || requester == null)
                {
                    return BadRequest("Пользователь не найден");
                }

                var request = _friendService.GetRequest(requester, addressee);
                if (request == null)
                {
                    return BadRequest("Заявка не найдена");
                }

                work(request);

                return Redirect("/friends/requests");
            }
            else
            {
                return Unauthorized("Ошибка проверки пользователя");
            }
        }

        [HttpGet("getFriendsRequests")]
        public List<FriendRequest> GetFriendsRequests()
        {
            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
            var requester = _userService.GetUserByTg(tgUser);
            
            //Перенести в сервис
            List<FriendRequest> result;
            using (var db = new ContextDataBase())
            {
                result = db.FriendRequests.Where(x => x.AddresseeId == requester.Id).ToList();
            }
            return result;
            //return Ok(requests);
        }

        [HttpPost("deleteFriend")]
        public IActionResult DeleteFriend(string tgId)
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

                var friend = _userService.GetUserByTgId(tgId);
                if (friend == null)
                {
                    return BadRequest("Друг не найден");
                }

                var friendList = _friendService.GetFriendList(user, friend);
                _friendService.DeleteFriendList(friendList);

                return Redirect($"/user/{tgId}");
            }
            else 
            {
                return Unauthorized("Ошибка проверки пользователя");
            }
        }

    }
}
