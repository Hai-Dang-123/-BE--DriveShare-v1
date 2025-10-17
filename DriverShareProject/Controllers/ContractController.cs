using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
       
        private readonly IContractService _contractService;
        public ContractController( IContractService contractService)
        {
            
            _contractService = contractService;
        }
        [HttpPost("Create Vehicle Contract")]
        public async Task<IActionResult> CreateVehicleContract(CreateVehicleContractDto dto)
        {
            var response = await _contractService.CreatVehicleContractAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("Create Item Contract")]
        public async Task<IActionResult> CreateItemContract(CreateItemContractDto dto)
        {
            var response = await _contractService.CreateItemContractAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
