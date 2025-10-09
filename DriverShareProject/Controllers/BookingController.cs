//using BLL.Services.Interface;
//using Common.DTOs;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace DriverShareProject.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class BookingController : ControllerBase
//    {
//        private readonly IBookingService _bookingService;

//        public BookingController(IBookingService bookingService)
//        {
//            _bookingService = bookingService;
//        }

//        // API tạo booking mới
//        [HttpPost]
//        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDTO dto)
//        {
//            var result = await _bookingService.CreateBookingAsync(dto);
//            return StatusCode(result.StatusCode, result);
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetAllBookings()
//        {
//            var result = await _bookingService.GetAllBookingsAsync();
//            return StatusCode(result.StatusCode, result);
//        }

//        // Lấy booking theo Id
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetBookingById(Guid id)
//        {
//            var result = await _bookingService.GetBookingByIdAsync(id);
//            return StatusCode(result.StatusCode, result);
//        }
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] CreateBookingDTO dto)
//        {
//            var result = await _bookingService.UpdateBookingAsync(id, dto);
//            return StatusCode(result.StatusCode, result);
//        }

//        // Xóa booking
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteBooking(Guid id)
//        {
//            var result = await _bookingService.DeleteBookingAsync(id);
//            return StatusCode(result.StatusCode, result);
//        }
//    }
//}