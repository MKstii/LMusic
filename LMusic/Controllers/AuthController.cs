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
                    var user = _userService.GetUserByTg(telegramUser, true);

                    var tgUserJson = JsonSerializer.Serialize(telegramUser);
                    var userJson = JsonSerializer.Serialize(user);
                    Response.Cookies.Append("TelegramUserHash", tgUserJson);
                    Response.Cookies.Append("User", userJson);

                    return Ok(user);
                }
                else
                {
                    return BadRequest("Auth error");
                }
                //_authService.LoginUser(telegramUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
    }
}
