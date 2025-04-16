using IndustryRegApi.Interface;
using IndustryRegApi.Models;
using IndustryRegApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndustryRegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentPlanController : ControllerBase
    {
        private readonly IInvestmentPlan _service;

        public InvestmentPlanController(IInvestmentPlan service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plan = await _service.GetInvestmentPlanByIdAsync(id);
            if (plan == null)
                return NotFound();
            return Ok(plan);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _service.GetAllInvestmentPlansAsync();
            return Ok(plans);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvestmentPlan plan)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPlan = await _service.CreateInvestmentPlanAsync(plan);
            return Ok(createdPlan);
        }
    }
}
