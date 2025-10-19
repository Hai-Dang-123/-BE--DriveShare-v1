using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemContractController : ControllerBase
    {
        private readonly IItemContractService _itemContractService;

        public ItemContractController(IItemContractService itemContractService)
        {
            _itemContractService = itemContractService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllItemContractsAsync()
        {
            var result = await _itemContractService.GetAllItemContractsAsync();
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemContractAsync(Guid id, [FromBody] CreateItemContractDto dto)
        {
            var result = await _itemContractService.UpdateItemContractAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemContract(Guid id)
        {
            var result = await _itemContractService.DeleteItemContractAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
