using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleContractController : ControllerBase
    {
        private readonly IVehicleContractService _service;

        public VehicleContractController(IVehicleContractService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicleContractsAsync()
        {
            var result = await _service.GetAllVehicleContractsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicleContractAsync(Guid id, [FromBody] CreateVehicleContractDto dto)
        {
            var result = await _service.UpdateVehicleContractAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleContractAsync(Guid id)
        {
            var result = await _service.DeleteVehicleContractAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
