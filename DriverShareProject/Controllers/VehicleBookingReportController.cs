using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleBookingReportController : ControllerBase
    {
        private readonly IVehicleBookingReportService _reportService;

        public VehicleBookingReportController(IVehicleBookingReportService reportService)
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
        public async Task<IActionResult> Create([FromBody] CreateVehicleBookingReportDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDTO("Dữ liệu không hợp lệ.", 400, false));
            }

            var response = await _reportService.CreateReportAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateVehicleBookingReportDTO dto)
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