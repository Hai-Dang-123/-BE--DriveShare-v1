using BLL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost("Create-Review")]
        public async Task<IActionResult> CreateReview([FromBody] Common.DTOs.CreateReviewDTO createReviewDTO)
        {
            var response = await _reviewService.CraeteReviewAsync(createReviewDTO);
            return StatusCode(response.StatusCode, response);
        }
       
        [HttpPut("Update-Review")]
        public async Task<IActionResult> UpdateReview([FromBody] Common.DTOs.UpdateReviewDTO updateReviewDTO)
        {
            var response = await _reviewService.UpdateReviewAsync(updateReviewDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("Delete-Review")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            var response = await _reviewService.DeleteReviewAsync(reviewId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get-Reviews-By-ToUserId")]
       
        public async Task<IActionResult> GetReviewsByToUserId(Guid toUserId)
        {
            var response = await _reviewService.GetReviewsByTouserIdAsync(toUserId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("Get-Review-By-Id")]
        
        public async Task<IActionResult> GetReviewById(Guid reviewId)
        {
            var response = await _reviewService.GetReviewByIdAsync(reviewId);
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("Get-All-Reviews")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAllReviews()
        {
            var response = await _reviewService.GetAllReviewByIdAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
    }
