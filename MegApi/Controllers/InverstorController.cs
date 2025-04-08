using MegApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InverstorController : ControllerBase
    {
        private IUserInvestors _userInvestors;

        public InverstorController(IUserInvestors userInvestors) { this._userInvestors = userInvestors; }
        [Authorize]
        [HttpGet("GetInvestors")]
        
        public IActionResult GetInvestors([FromQuery] string userId)
        {
            try
            {

                var investors = _userInvestors.GetInvestors(userId);

                if (investors == null || !investors.Any())
                {
                    return NotFound("No investors found.");
                }

                return Ok(investors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving investors.");
            }
        }

    }
}
