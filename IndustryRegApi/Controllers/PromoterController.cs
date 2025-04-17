using IndustryRegApi.Interface;
using IndustryRegApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndustryRegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoterController : ControllerBase
    {
        private readonly IPromoter _promoterService;

        public PromoterController(IPromoter promoterService)
        {
            _promoterService = promoterService;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Promoter> GetPromoter(int userId)
        {
            var promoter = _promoterService.GetPromoter(userId);

            if (promoter == null)
            {
                return NotFound();
            }

            return Ok(promoter);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> CreatePromoter([FromBody] Promoter promoter)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _promoterService.CreatePromoter(promoter);

                return CreatedAtAction(nameof(GetPromoter), new { userId = promoter.IDD_ID }, result);
            }
            catch (Exception ex)
            {
                // Log the exception here if you have a logging system
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while creating the promoter", error = ex.Message });
            }
        }
    }
}
