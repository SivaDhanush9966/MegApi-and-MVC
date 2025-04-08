using MegApi.Interfaces;
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

        [HttpGet("GetInvestors")]
        public IActionResult actionResult()
        {
            try
            {
                var investors = _userInvestors.GetInvestors();

                if (investors == null || !investors.Any())
                {
                    return NotFound("No investors found.");
                }

                return Ok(investors);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
