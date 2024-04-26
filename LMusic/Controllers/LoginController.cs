using LMusic.Models.Requests;
using LMusic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace LMusic.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet("GetAuthResult")]
        public IActionResult Auth(TelegrammUser user)
        {
            try
            {
                using (AuthService service = new AuthService())
                {
                    if (!Request.Cookies.ContainsKey("UserHash"))
                    {
                        var us = service.LoginUser(user);
                        Response.Cookies.Append("UserHash", us.ToString()); 
                    }
                    else
                    {
                        var us = service.LoginUser(user);
                    }
                    return null; // а че возвращать?
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }
        
    }
}
