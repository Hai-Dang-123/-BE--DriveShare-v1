using BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// ❌ Xóa một biên bản thực tế (Report)
        /// </summary>
        /// <param name="id">🔸 ReportId của biên bản cần xóa</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            var result = await _reportService.DeleteReportAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
