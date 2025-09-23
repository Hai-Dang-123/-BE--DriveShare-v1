using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser (CreateUserDTO dto)
        {
            var response = await _userService.CreateUserAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // DELETE: api/User
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            var response = await _userService.DeleteUserAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}
