using BLL.Services.Implement;
using BLL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCharacteristicsController : ControllerBase
    {
        private readonly IItemCharacteristicsService _itemCharacteristicsService;
        public ItemCharacteristicsController(IItemCharacteristicsService itemCharacteristicsService)
        {
            _itemCharacteristicsService = itemCharacteristicsService;
        }
        [HttpPost("create-item-characteristics")]
        public async Task<IActionResult> CreateItemCharacteristics([FromBody] Common.DTOs.CreateItemCharacteristicsDTO createItemCharacteristicsDTO)
        {
            var result = await _itemCharacteristicsService.CreateItemCharacteristicsAsnc(createItemCharacteristicsDTO);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut("update-item-characteristics")]
        public async Task<IActionResult> UpdateItemCharacteristics([FromBody] Common.DTOs.CreateItemCharacteristicsDTO updateItemCharacteristicsDTO)
        {
            var result = await _itemCharacteristicsService.UpdateItemCharacteristicsAsync(updateItemCharacteristicsDTO);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("get-item-characteristics/{itemId}")]
        public async Task<IActionResult> GetItemCharacteristicsByItemId([FromRoute] Guid itemId)
        {
            var result = await _itemCharacteristicsService.GetItemCharacteristicsByIdAsync(itemId);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("get-all-item-characteristics")]
        public async Task<IActionResult> GetAllItemCharacteristics()
        {
            var result = await _itemCharacteristicsService.GetAllItemCharacteristicsAsync();
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePostItem(Guid ItemCharacteristicId)
        {
            var response = await _itemCharacteristicsService.DeleteItemCharacteristicsAsync(ItemCharacteristicId);

            return StatusCode(response.StatusCode, response);
        }

    }
}
