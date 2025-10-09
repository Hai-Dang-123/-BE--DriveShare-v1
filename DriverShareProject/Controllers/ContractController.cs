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
        public ContractController(IContractTemplateService contractTemplateService)
        {
            _contractTemplateService = contractTemplateService;
        }
        [HttpPost("Create Contract Template")]
        public async Task<IActionResult> CreateContractTemplate(ContractTemplateDTO dto)
        {
            var response = await _contractTemplateService.CreateContractTemplateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
