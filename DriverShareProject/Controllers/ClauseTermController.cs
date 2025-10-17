using BLL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClauseTermController : ControllerBase
    {
        private readonly IClauseTermServices _clauseTermServices;
        public ClauseTermController(IClauseTermServices clauseTermServices)
        {
            _clauseTermServices = clauseTermServices;
        }
        [HttpPost("CreateClauseTerm")]
        public async Task<IActionResult> CreateClauseTerm([FromBody] Common.DTOs.ClauseTermDTO clauseTermDTO)
        {
            var response = await _clauseTermServices.CreateClauseTermAync(clauseTermDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("UpdateClauseTerm")]
        public async Task<IActionResult> UpdateClauseTerm([FromBody] Common.DTOs.UpdateClauseTermDTO clauseTermDTO)
        {
            var response = await _clauseTermServices.UpdateClauseTermAsync(clauseTermDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("DeleteClauseTerm")]
        public async Task<IActionResult> DeleteClauseTerm([FromQuery] Guid clauseTermId)
        {
            var response = await _clauseTermServices.DeleteClauseTermAsync(clauseTermId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetAllClauseTerms")]
        public async Task<IActionResult> GetAllClauseTerms()
        {
            var response = await _clauseTermServices.GetAllClauseTermsAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetClauseTermById")]
        public async Task<IActionResult> GetClauseTermById([FromQuery] Guid clauseTermId)
        {
            var response = await _clauseTermServices.GetClauseTermByIdAsync(clauseTermId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
