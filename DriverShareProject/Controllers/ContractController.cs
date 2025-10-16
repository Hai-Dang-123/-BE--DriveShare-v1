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
        private readonly IContractTemplateService _contractTemplateService;
        private readonly IContractService _contractService;
        public ContractController(IContractTemplateService contractTemplateService, IContractService contractService)
        {
            _contractTemplateService = contractTemplateService;
            _contractService = contractService;
        }
        [HttpPost("Create Contract Template")]
        public async Task<IActionResult> CreateContractTemplate(ContractTemplateDTO dto)
        {
            var response = await _contractTemplateService.CreateContractTemplateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get All Contract Template")]
        public async Task<IActionResult> GetAllContractTemplate()
        {
            var response = await _contractTemplateService.GetAllContractTemplateasync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get Contract Template By Id")]
        public async Task<IActionResult> GetContractTemplateById(Guid id)
        {
            var response = await _contractTemplateService.GetContractTemplateByIdAsync(id);
            return StatusCode(response.StatusCode, response);
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
