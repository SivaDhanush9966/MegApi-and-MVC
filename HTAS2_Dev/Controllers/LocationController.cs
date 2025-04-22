using HTAS2_Dev.BussinessLayer.Interface;
using HTAS2_Dev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTAS2_Dev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IStateMasterService _stateMasterService;

        public LocationController(IStateMasterService stateMasterService)
        {
            _stateMasterService = stateMasterService;
        }

        [HttpGet("states")]
        public IActionResult GetAllStates()
        {
            var states = _stateMasterService.GetAllStates();

            if (states == null || states.Count == 0)
            {
                return NotFound("No states found.");
            }

            return Ok(states);
        }
    }
}

