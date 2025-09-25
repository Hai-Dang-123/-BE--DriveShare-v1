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

        [HttpPost]
        public async Task<IActionResult> CreatePostVehicale(CreateRequestPostVehicleDTO dto)
        {
            var response = await _postVehicleService.CreatePostVehicleAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
