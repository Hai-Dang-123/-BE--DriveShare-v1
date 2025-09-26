using BLL.Services.Implement;
using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostVehicleController : ControllerBase
    {
        private readonly IPostVehicleService _postVehicleService;
        public PostVehicleController( IPostVehicleService postVehicleService)
        {
            _postVehicleService = postVehicleService;
        }

        [HttpPost("Create PostVehicle")]
        public async Task<IActionResult> CreatePostVehicle(CreateRequestPostVehicleDTO dto)
        {
            var response = await _postVehicleService.CreatePostVehicleAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("Update PostVehicle")]
        public async Task<IActionResult> UpdatePostVehicle(UpdateRequestPostVehicleDTO dto)
        {
            var response = await _postVehicleService.UpdatePostVehicleAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get All PostVehicles of Owner")]
        public async Task<IActionResult> GetAllPostVehiclesOfOwner()
        {
            var response = await _postVehicleService.GetAllPostVehiclesOwner();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get PostVehicle by Id")]
        public async Task<IActionResult> GetPostVehicleById(Guid postId)
        {
            var response = await _postVehicleService.GetPostVehicleByIdAsync(postId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("Delete PostVehicle")]
        public async Task<IActionResult> DeletePostVehicle(Guid postId)
        {
            var response = await _postVehicleService.DeletePostVehicleAsync(postId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
