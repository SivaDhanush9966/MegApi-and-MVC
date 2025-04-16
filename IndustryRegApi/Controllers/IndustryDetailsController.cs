using IndustryRegApi.Interface;
using IndustryRegApi.Models;
using IndustryRegApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndustryRegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryDetailsController : ControllerBase
    {
        private readonly IIndustryDetails _service;

        public IndustryDetailsController(IIndustryDetails service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetIndustryDetailsByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllIndustryDetailsAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IndustryDetails details)
        {
            var created = await _service.CreateIndustryDetails(details);
            return CreatedAtAction(nameof(GetById), new { id = created.RevProjID }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] IndustryDetails details)
        {
            var updated = await _service.UpdateIndustryDetailsAsync(id, details);
            if (!updated) return NotFound();
            return NoContent();
        }
    }
}
