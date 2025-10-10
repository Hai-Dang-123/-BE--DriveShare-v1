using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClauseController : ControllerBase
    {
        private readonly IClausesTemplateService _clausesService;
        public ClauseController(IClausesTemplateService clausesService)
        {
            _clausesService = clausesService;
        }
        [HttpPost("Create-Clause")]
        public async Task<IActionResult> CreateClause(CreateClauseTemplateDTO createClauseDTO)
        {
            var response = await _clausesService.CreateClauseAsync(createClauseDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("Update-Clause")]
        public async Task<IActionResult> UpdateClause(UpdateClauseTemplateDTO updateClauseDTO)
        {
            var response = await _clausesService.UpdateClauseAsync(updateClauseDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get-Clause-By-id")]
        public async Task<IActionResult> GetClauseByIdAsync(Guid clauseId)
        {
            var response = await _clausesService.GetClauseByIdAsync(clauseId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get-All-Clause")]
        public async Task<IActionResult> GetAllClauseAsync()
        {
            var response = await _clausesService.GetAllClausesAsync();
            return StatusCode(response.StatusCode, response);
        }



    }
}
