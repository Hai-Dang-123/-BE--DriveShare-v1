using BLL.Services.Implement;
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
        [HttpGet("get-item/{itemId}")]
        public async Task<IActionResult> GetItemById([FromRoute] Guid itemId)
        {
            var result = await _itemServices.GetItemByIdAsync(itemId);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("get-all-items")]
        public async Task<IActionResult> GetAllItems()
        {
            var result = await _itemServices.GetAllItemsAsync();
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePostItem(Guid ItemId)
        {
            var response = await _itemServices.DeleteItemAsync(ItemId);

            return StatusCode(response.StatusCode, response);
        }
    }
    }
