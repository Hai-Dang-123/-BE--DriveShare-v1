using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("Create Contract Terms")]
        public async Task<IActionResult> CreateContractTerms(ContracttermDTO contractTermsDTO)
        {
            var response = await _contractTermService.CreateContractTermsAsync(contractTermsDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get All Contract Terms By Template Id")]
        public async Task<IActionResult> GetAllContractTerms(Guid contractTemplateId)
        {
            var response = await _contractTermService. GetAllContractTermsAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get Contract Terms By Id")]
        public async Task<IActionResult> GetContractTermById(Guid contractTermId)
        {
            var response = await _contractTermService.GetContractTermsAsync(contractTermId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("Update Contract Terms")]
        public async Task<IActionResult> UpdateContractTerms(UpdateContracttermDTO updateContractTermDTO)
        {
            var response = await _contractTermService.UpdateContractTermsAsync(updateContractTermDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("Delete Contract Terms")]
        public async Task<IActionResult> DeleteContractTerms(Guid contractTermId)
        {
            var response = await _contractTermService.DeleteContractTermsAsync(contractTermId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
