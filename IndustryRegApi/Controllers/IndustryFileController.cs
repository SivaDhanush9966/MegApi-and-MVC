using IndustryRegApi.Interface;
using IndustryRegApi.Models;
using IndustryRegApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndustryRegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryFileController : ControllerBase
    {
     private readonly IIndustryFile _fileService;

        public IndustryFileController(IIndustryFile fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IndustryFileDetails fileDetails)
        {
            try
            {
                var result = await _fileService.SaveIndustryFileDetailsAsync(fileDetails);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error uploading file", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var files = await _fileService.GetAllFilesAsync();
            return Ok(files);
        }

        [HttpGet("{slno}")]
        public async Task<IActionResult> GetById(int slno)
        {
            try
            {
                var fileDetails = await _fileService.GetFileDetailsByIdAsync(slno);
                if (fileDetails == null)
                    return NotFound(new { message = "File details not found" });

                return Ok(fileDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving data", details = ex.Message });
            }
        }

    }

}
