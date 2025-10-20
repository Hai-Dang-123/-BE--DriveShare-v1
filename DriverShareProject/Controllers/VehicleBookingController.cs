using BLL.Services.Interface;
using Common.DTOs;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [ApiController]
    [Route("api/vehicle-bookings")]
    public class VehicleBookingController : ControllerBase
    {
        private readonly IVehicleBookingService _vehicleBookingService;

        public VehicleBookingController(IVehicleBookingService vehicleBookingService)
        {
            _vehicleBookingService = vehicleBookingService;
        }

        // 🟢 Tạo booking xe
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDTO dto)
        {
            var response = await _vehicleBookingService.CreateVehicleBookingAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // 🟢 Cập nhật trạng thái booking
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] BookingStatus status)
        {
            var response = await _vehicleBookingService.ChangeStatusAsync(id, status);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("my-bookings")]
        public async Task<IActionResult> GetMyBookings()
        {
            var response = await _vehicleBookingService.GetBookingsByCurrentUserAsync();
            return StatusCode(response.StatusCode, response);
        }

        // ✅ Lấy chi tiết booking theo VehicleBookingId (phải là booking của user này)
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingById(Guid bookingId)
        {
            var response = await _vehicleBookingService.GetBookingByIdAsync(bookingId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
