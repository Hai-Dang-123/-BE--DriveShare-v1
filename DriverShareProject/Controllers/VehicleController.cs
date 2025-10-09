using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromForm] CreateVehicleDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseDTO("Validation failed", 400, false, ModelState));

            var response = await _vehicleService.CreateVehicleAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var response = await _vehicleService.GetAllVehiclesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleById(Guid id)
        {
            var response = await _vehicleService.GetVehicleByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVehicle([FromForm] UpdateVehicleDTO dto)
        {
            var response = await _vehicleService.UpdateVehicleAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            var response = await _vehicleService.DeleteVehicleAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("status")]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeVehicleStatusDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseDTO("Invalid input", 400, false, ModelState));

            var response = await _vehicleService.ChangeStatusAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

    }
}

