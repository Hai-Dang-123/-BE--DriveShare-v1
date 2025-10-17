using BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetAllContractTemplates()
        {
            var response = await _contractTemplateService.GetAllContractTemplateasync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractTemplateById(Guid id)
        {
            var response = await _contractTemplateService.GetContractTemplateByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
