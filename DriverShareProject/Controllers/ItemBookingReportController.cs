using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemBookingReportController : ControllerBase
    {
        private readonly IItemBookingReportService _service;

        public ItemBookingReportController(IItemBookingReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllItemReportsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
            => Ok(await _service.GetItemReportByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemBookingReportDTO dto)
            => Ok(await _service.CreateItemReportAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateItemBookingReportDTO dto)
            => Ok(await _service.UpdateItemReportAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
            => Ok(await _service.DeleteItemReportAsync(id));
    }
}
