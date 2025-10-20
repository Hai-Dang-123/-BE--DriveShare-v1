using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    public class ClauseTemplateController : ControllerBase
    {
        private readonly IClausesTemplateService _clauseTemplateService;
        public ClauseTemplateController(IClausesTemplateService clauseTemplateService)
        {
            _clauseTemplateService = clauseTemplateService;
        }
        [HttpPost("Create-Clause")]
        public async Task<IActionResult> CreateClause(CreateClauseTemplateDTO createClauseDTO)
        {
            var response = await _clauseTemplateService.CreateClauseTemplateAsync(createClauseDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("Update-Clause")]
        public async Task<IActionResult> UpdateClause(UpdateClauseTemplateDTO updateClauseDTO)
        {
            var response = await _clauseTemplateService.UpdateClauseTemplateAsync(updateClauseDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get-Clause-By-id")]
        public async Task<IActionResult> GetClauseByIdAsync(Guid clauseId)
        {
            var response = await _clauseTemplateService.GetClauseTemplateByIdAsync(clauseId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get-All-Clause")]
        public async Task<IActionResult> GetAllClauseAsync()
        {
            var response = await _clauseTemplateService.GetAllClausesTemplateAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("Delete-Clause")]
        public async Task<IActionResult> DeleteClauseAsync(Guid clauseId)
        {
            var response = await _clauseTemplateService.DeleteClauseTemplateAsync(clauseId);
            return StatusCode(response.StatusCode, response);
        }


    }
}
