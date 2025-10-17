using BLL.Services.Interface;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DriverShareProject.Controllers
{
    [Route("api")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IRuleService _ruleService;

        public RuleController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        [HttpGet("rules")]
        public async Task<IActionResult> GetAllRules()
        {
            var response = await _ruleService.GetAllRulesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("rules/{id}")]
        public async Task<IActionResult> GetRuleById(Guid id)
        {
            var response = await _ruleService.GetRuleByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("rule")]
        public async Task<IActionResult> CreateRule([FromBody] CreateRuleDTO dto)
        {
            var response = await _ruleService.CreateRuleAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("rule/{id}")]
        public async Task<IActionResult> UpdateRule(Guid id, [FromBody] UpdateRuleDTO dto)
        {
            var response = await _ruleService.UpdateRuleAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("rule/{id}")]
        public async Task<IActionResult> DeleteRule(Guid id)
        {
            var response = await _ruleService.DeleteRuleAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
