using BLL.Services.Interface;

using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTemplateController : ControllerBase
    {
        private readonly IContractTemplateService _contractTemplateService;

        public ContractTemplateController(IContractTemplateService contractTemplateService)
        {
            _contractTemplateService = contractTemplateService;
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
    }
}
