using Common.DTOs;

namespace BLL.Services.Interface
{
    public interface IRuleService
    {
        Task<ResponseDTO> GetAllRulesAsync();
        Task<ResponseDTO> GetRuleByIdAsync(Guid id);
        Task<ResponseDTO> CreateRuleAsync(CreateRuleDTO dto);
        Task<ResponseDTO> UpdateRuleAsync(Guid id, UpdateRuleDTO dto);
        Task<ResponseDTO> DeleteRuleAsync(Guid id);
    }
}
