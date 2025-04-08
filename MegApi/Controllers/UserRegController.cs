using MegApi.Interfaces;
using MegApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegController : ControllerBase
    {
        private IUserRegService _userService;
        private readonly IConfiguration _config;

        public UserRegController(IUserRegService userRegService, IConfiguration config)
        {
            _userService = userRegService;
            _config = config;
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
                    var token = GenerateJwtToken(response); 
                    return Ok(new
                    {
                        Message = "Login successful",
                        Token = token,     
                        Data = response
                    });
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

        private string GenerateJwtToken(UserInfo user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, user.Fullname),
               new Claim(ClaimTypes.Name, user.Userid)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
