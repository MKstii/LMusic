using LMusic.Models.Requests;
using LMusic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Text.Encodings.Web;
using System.Web;
using System.Text.Json;


namespace LMusic.Controllers
{

    public class AuthController : Controller
    {
        private AuthService _authService = new AuthService();
        private UserService _userService = new UserService();

        [HttpGet("GetAuthResult")]
        public IActionResult Auth(TelegrammUser telegramUser)
        {
            try
            {
                if (_authService.ValidUser(telegramUser))
                {
                    var user = _userService.GetUserByTg(telegramUser);

                    if(user == null)
                    {
                        user = _userService.CreateUserByTg(telegramUser);
                    }

                    var tgUserJson = JsonSerializer.Serialize(telegramUser);
                    Response.Cookies.Append("TelegramUserHash", tgUserJson);

                    // поменять на страницу юзера
                    return Redirect("/user");
                }
                else
                {
                    return BadRequest("Auth error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("TelegramUserHash");
            return Redirect("/home");
        }

    }
}
