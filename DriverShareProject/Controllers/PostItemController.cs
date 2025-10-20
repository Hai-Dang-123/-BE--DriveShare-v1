using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostItemController : ControllerBase
    {
        private readonly IPostItemService _postItemService;
        public PostItemController(IPostItemService postItemService)
        {
            _postItemService = postItemService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePostItem(CreatePostItemDTO createPostItemDTO)
        {
            var response = await _postItemService.CreatePostItemAsync(createPostItemDTO);
        
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePostItem( Guid postItemId)
        {
            var response = await _postItemService.DeletePostItemAsync(postItemId);
        
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePostItem(UpdatePostItemDTO updatePostItemDTO)
        {
            var response = await _postItemService.UpdatePostItemAsync(updatePostItemDTO);
        
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("get/{postItemId}")]
        public async Task<IActionResult> GetPostItemById(Guid postItemId)
        {
            var response = await _postItemService.GetPostItemByIdAsync(postItemId);
        
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPostItems()
        {
            var response = await _postItemService.GetAllPostItemsAsync();
        
            return StatusCode(response.StatusCode, response);
        }
    }
}
