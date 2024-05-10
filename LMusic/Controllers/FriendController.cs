//using LMusic.Models;
//using LMusic.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace LMusic.Controllers
//{
//    public class FriendController : Controller
//    {
//        private UserService _userService = new UserService();
//        private AuthService _authService = new AuthService();
//        private FriendService _friendService = new FriendService();
//        [HttpPost("ChangeAvatar")]
//        public IActionResult SendFriendRequest(int tgId)
//        {
//            var tgUserJson = Request.Cookies["TelegramUserHash"] != null ? Request.Cookies["TelegramUserHash"] : null;
//            var tgUser = _userService.ConvertJsonToTgUser(tgUserJson);
//            if (_authService.ValidUser(tgUser))
//            {
//                var requester = _userService.GetUserByTg(tgUser);
//                var addressee = _userService.GetUserByTgId(tgId);

//                if(requester == null || addressee == null)
//                {
//                    return BadRequest("Пользователь не найден");
//                }

//                //if (_friendService.HasRequest(requester, addressee))
//                //{
//                //    return BadRequest("Заявка уже существует");
//                //}

//                _friendService.AddRequest(requester, addressee);

//                return Ok();
//            }
//            else
//            {
//                return Unauthorized("Ошибка проверки пользователя");
//            }
                
//        }
//    }
//}
