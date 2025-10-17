using BLL.Services.Interface;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemContract(Guid id)
        {
            var result = await _itemContractService.DeleteItemContractAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
