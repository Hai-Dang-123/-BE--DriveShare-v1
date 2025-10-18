using BLL.Services.Interface;
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
