using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemBookingReportController : ControllerBase
    {
        private readonly IItemBookingReportService _reportService;

        public ItemBookingReportController(IItemBookingReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _reportService.GetAllReportsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _reportService.GetReportByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemBookingReportDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDTO("Dữ liệu không hợp lệ.", 400, false));
            }

            var response = await _reportService.CreateReportAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateItemBookingReportDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDTO("Dữ liệu không hợp lệ.", 400, false));
            }

            var response = await _reportService.UpdateReportAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _reportService.DeleteReportAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}