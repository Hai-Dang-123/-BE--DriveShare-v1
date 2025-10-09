using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var response = await _authService.LoginAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // POST: api/Auth/register  
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }

}
