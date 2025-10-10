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
        [HttpPost("Create Clause")]
        public async Task<IActionResult> CreateClause(CreateClauseTemplateDTO createClauseDTO)
        {
            var response = await _clausesService.CreateClauseAsync(createClauseDTO);
            return StatusCode(response.StatusCode, response);
        }
        //[HttpPut("Update Clause")]
        //public async Task<IActionResult> UpdateClause([FromBody] Common.DTOs.UpdateClauseDTO updateClauseDTO)
        //{
        //    var response = await _clausesService.UpdateClauseAsync(updateClauseDTO);
        //    return StatusCode(response.StatusCode, response);
        //}
        //[HttpGet("Get All Clauses")]
        //public async Task<IActionResult> GetAllClauses()
        //{
        //    var response = await _clausesService.GetAllClauseAsync();
        //    return StatusCode(response.StatusCode, response);
        //}
        //[HttpGet("Get Clause by Id")]
        //public async Task<IActionResult> GetClauseById(Guid id)
        //{
        //    var response = await _clausesService.GetClauseByIdAsync(id);
        //    return StatusCode(response.StatusCode, response);
        //}
        //[HttpDelete("Delete Clause")]
        //public async Task<IActionResult> DeleteClause(Guid id)
        //{
        //    var response = await _clausesService.DeleteClauseAsync(id);
        //    return StatusCode(response.StatusCode, response);
        //}

    }
}
