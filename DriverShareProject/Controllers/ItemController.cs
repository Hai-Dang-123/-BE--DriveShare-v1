using BLL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IITemServices _itemServices;
        public ItemController(IITemServices itemServices)
        {
            _itemServices = itemServices;
        }
        [HttpPost("create-item")]
        public async Task<IActionResult> CreateItem([FromBody] Common.DTOs.CreateItemDTO createItemDTO)
        {
            var result = await _itemServices.CreateItemAsync(createItemDTO);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut("update-item")]
        public async Task<IActionResult> UpdateItem([FromBody] Common.DTOs.UpdateItemDTO updateItemDTO)
        {
            var result = await _itemServices.UpdateItemAsync(updateItemDTO);
            return StatusCode(result.StatusCode, result);
        }
    }
}
