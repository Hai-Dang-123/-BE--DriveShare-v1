using BLL.Services.Implement;
using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [ApiController]
    [Route("api/report-term")]
    public class ReportTermController : ControllerBase
    {
        private readonly IReportTermService _reportTermService;

        public ReportTermController(IReportTermService reportTermService)
        {
            _reportTermService = reportTermService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateReportTerm([FromBody] ReportTermDTO dto)
        {
            var result = await _reportTermService.CreateReportTermAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateReportTerm(Guid id, [FromBody] ReportTermDTO dto)
        {
            var result = await _reportTermService.UpdateReportTermAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReportTerms()
        {
            var response = await _reportTermService.GetAllReportTermsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportTermById(Guid id)
        {
            var response = await _reportTermService.GetReportTermByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportTerm(Guid id)
        {
            var response = await _reportTermService.DeleteReportTermAsync(id);
            return StatusCode(response.StatusCode, response);
        }


    }
}
