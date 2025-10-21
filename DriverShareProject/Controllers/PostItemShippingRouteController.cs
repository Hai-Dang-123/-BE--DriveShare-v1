using BLL.Services.Implement;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostItemShippingRouteController : ControllerBase
    {
        private readonly IPostItemShippingRouteService _shippingService;
        public PostItemShippingRouteController(IPostItemShippingRouteService shippingService)
        {
            _shippingService = shippingService;
        }
        [HttpPost("Create-PostItemShippingRoute")]
        public async Task<IActionResult> CreatePostItemShippingRoute( CreatePostItemShippingRouteRequest request)
        {
            var response = await _shippingService.CreatePostItemShippingRouteAsync(request);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpGet("Get-All-PostItemShippingRoute")]
        public async Task<IActionResult> GetAllPostItemShippingRoutes()
        {
            var response = await _shippingService.GetALLPostItemShippingRouteAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Get-PostItemShippingRoute/{id}")]
        public async Task<IActionResult> GetPostItemShippingRouteById(Guid id)
        {
            var response = await _shippingService.GetByIdPostItemShippingRouteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
     
        [HttpPut("Update-PostItemShippingRoute")]
        public async Task<IActionResult> UpdatePostItemShippingRoute( CreatePostItemShippingRouteRequest request)
        {
            var response = await _shippingService.UpdatePostItemShippingRouteAsync(request);
            return StatusCode(response.StatusCode, response);
        }

    }
}
