using BLL.Services.Implement.BLL.Services.Implement;
using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddOptionController : ControllerBase
    {
        private readonly IAddOptionService _addOptionService;

        public AddOptionController(IAddOptionService addOptionService)
        {
            _addOptionService = addOptionService;
        }

        // GET: api/AddOption
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _addOptionService.GetAllAddOptionsAsync();
            return StatusCode(response.StatusCode, response);
        }

        // GET: api/AddOption/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _addOptionService.GetAddOptionByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // POST: api/AddOption
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAddOptionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDTO("Dữ liệu không hợp lệ.", 400, false));
            }

            var response = await _addOptionService.CreateAddOptionAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // PUT: api/AddOption/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateAddOptionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDTO("Dữ liệu không hợp lệ.", 400, false));
            }

            var response = await _addOptionService.UpdateAddOptionAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        // DELETE: api/AddOption/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _addOptionService.DeleteAddOptionAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}