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
        private readonly IVehicleBookingReportService _service;

        public VehicleBookingReportController(IVehicleBookingReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicleBookingReportsAsync()
        {
            var result = await _service.GetAllVehicleBookingReportsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleBookingReportByIdAsync(Guid id)
        {
            var result = await _service.GetVehicleBookingReportByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicleBookingReportAsync([FromBody] CreateVehicleBookingReportDTO dto)
        {
            var result = await _service.CreateVehicleBookingReportAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicleBookingReportAsync(Guid id, [FromBody] CreateVehicleBookingReportDTO dto)
            {
            var result = await _service.UpdateVehicleBookingReportAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleBookingReportAsync(Guid id)
        {
            var result = await _service.DeleteVehicleBookingReportAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}