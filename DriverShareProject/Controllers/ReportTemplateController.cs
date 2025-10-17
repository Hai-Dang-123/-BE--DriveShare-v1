using BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [ApiController]
    [Route("api/report-template")]
    public class ReportTemplateController : ControllerBase
    {
        private readonly IReportTemplateService _reportTemplateService;

        public ReportTemplateController(IReportTemplateService reportTemplateService)
        {
            _reportTemplateService = reportTemplateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReportTemplates()
        {
            var response = await _reportTemplateService.GetAllReportTemplatesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportTemplateById(Guid id)
        {
            var response = await _reportTemplateService.GetReportTemplateByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportTemplate(Guid id)
        {
            var response = await _reportTemplateService.DeleteReportTemplateAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
