using System.ComponentModel;
using System.Net;
using MegApi.Interfaces;
using MegApi.Models;
using MegApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly ILicense _licenseService;

        public LicenseController(ILicense licenseService)
        {
            _licenseService = licenseService;
        }

        [HttpPost("InsL1")]
        public IActionResult UpsertLicense([FromBody] LicenseeFullDetails license)
        {
            if (license == null)
            {
                return BadRequest("License data is null.");
            }

            // Get IP Address from request
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            if(license.Createdby==null && license.CreatedIp == null)
            {
                // Hardcoding CreatedBy for example purpose; you could extract from auth token in real apps
                license.Createdby = "System"; // You can fetch this from JWT or user session later
                license.CreatedIp = ipAddress ?? "Unknown";
            }
            

            try
            {
                var result = _licenseService.CreateLicense(license);
                return Ok(new { message = "License record inserted/updated successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred.", error = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetLicenseeById(int userId)
        {
            var licensee = _licenseService.GetLicenseeById(userId);

            if (licensee == null)
                return NotFound("Licensee not found");

            return Ok(licensee);
        }
    }
}
