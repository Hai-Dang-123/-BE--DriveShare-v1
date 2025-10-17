using BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTermController : ControllerBase
    {
        private readonly IContractTermService _contractTermService;

        public ContractTermController(IContractTermService contractTermService)
        {
            _contractTermService = contractTermService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContractTerms()
        {
            var response = await _contractTermService.GetAllContractTermsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractTermById(Guid id)
        {
            var response = await _contractTermService.GetContractTermByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
