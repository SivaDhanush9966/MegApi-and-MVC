using HTAS2_Dev.BussinessLayer.Interface;
using HTAS2_Dev.DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HTAS2_Dev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRoleMasterService _roleService;

        public UserController(IRoleMasterService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }
    }
}
