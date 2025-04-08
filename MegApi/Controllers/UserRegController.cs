using MegApi.Interfaces;
using MegApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegController : ControllerBase
    {
        private IUserRegService _userService;

        public UserRegController(IUserRegService userRegService)
        {
            _userService = userRegService;
        }
        [HttpPost("UserRegister")]
        public IActionResult Register([FromBody] UserRegDetails user)
        {
            try
            {
                string response = _userService.InsertUserRegDetails(user);

                return Ok(new { Message = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
        [HttpPost("UserLogin")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";
                var response = _userService.GetUserInfo(username, password, ipAddress);

                if (response != null && !string.IsNullOrEmpty(response.Userid))
                {
                    return Ok(new { Message = "Login successful", Data = response });
                }
                else
                {
                    return Unauthorized(new { Message = "Invalid username or password" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Details = ex.Message });
            }
        }
    }
}
