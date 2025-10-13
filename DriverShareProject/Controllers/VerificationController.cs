using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [ApiController]
    [Route("api/vehicle/verification")]
    public class VerificationController : ControllerBase
    {
        private readonly IVerificationService _verificationService;

        public VerificationController(IVerificationService verificationService)
        {
            _verificationService = verificationService;
        }

        // ✅ STEP 1: Upload giấy tờ lên Firebase
        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> Upload([FromForm] UploadVerificationDTO dto)
        {
            var result = await _verificationService.UploadDocumentsAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        // ✅ STEP 2: Gửi ảnh sang OCR VNPT
        [HttpPost("ocr/{verificationId}")]
        [Authorize]
        public async Task<IActionResult> Ocr(Guid verificationId)
        {
            var result = await _verificationService.SendOcrRequestAsync(verificationId);
            return StatusCode(result.StatusCode, result);
        }

        // ✅ STEP 3: Lưu bản ghi xác minh (sau OCR)
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateVerificationDTO dto)
        {
            var result = await _verificationService.CreateVerificationAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        // ✅ STEP 4: Lấy danh sách xác minh của 1 xe
        [HttpGet("vehicle/{vehicleId}")]
        [Authorize]
        public async Task<IActionResult> GetByVehicle(Guid vehicleId)
        {
            var result = await _verificationService.GetVerificationByVehicleIdAsync(vehicleId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
