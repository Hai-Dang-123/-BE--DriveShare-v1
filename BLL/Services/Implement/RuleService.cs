using BLL.Services.Interface;
using Common.DTOs;
using Common.Enums;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class RuleService : IRuleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RuleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> GetAllRulesAsync()
        {
            try
            {
                var rules = await _unitOfWork.RuleRepo.GetAll().ToListAsync();

                if (!rules.Any())
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Không có quy tắc nào."
                    };
                }

                var result = rules.Select(r => new RuleDTO
                {
                    RuleId = r.RuleId,
                    Name = r.Name,
                    Description = r.Description,
                    Category = r.Category,
                    Value = r.Value,
                    Unit = r.Unit,
                    AppliedScopeType = r.AppliedScopeType,
                    AppliedScopeId = r.AppliedScopeId,
                    EffectiveFrom = r.EffectiveFrom,
                    EffectiveTo = r.EffectiveTo,
                    Status = r.Status
                }).ToList();

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Lấy danh sách quy tắc thành công.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = "Lỗi khi lấy danh sách quy tắc: " + ex.Message
                };
            }
        }
        public async Task<ResponseDTO> GetRuleByIdAsync(Guid id)
        {
            try
            {
                var rule = await _unitOfWork.RuleRepo.GetByIdAsync(id);

                if (rule == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Không tìm thấy quy tắc."
                    };
                }

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Lấy chi tiết quy tắc thành công.",
                    Result = rule
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = "Lỗi khi lấy quy tắc: " + ex.Message
                };
            }
        }
        public async Task<ResponseDTO> CreateRuleAsync(CreateRuleDTO dto)
        {
            try
            {
                var rule = new Rule
                {
                    RuleId = Guid.NewGuid(),
                    Name = dto.Name,
                    Description = dto.Description,
                    Category = dto.Category,
                    Value = dto.Value,
                    Unit = dto.Unit,
                    AppliedScopeType = dto.AppliedScopeType,
                    AppliedScopeId = dto.AppliedScopeId,
                    EffectiveFrom = dto.EffectiveFrom,
                    EffectiveTo = dto.EffectiveTo,
                    Status = RuleStatus.ACTIVE,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.RuleRepo.AddAsync(rule);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "Tạo quy tắc thành công.",
                    Result = rule
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = "Lỗi khi tạo quy tắc: " + ex.Message
                };
            }
        }
        public async Task<ResponseDTO> UpdateRuleAsync(Guid id, UpdateRuleDTO dto)
        {
            try
            {
                var rule = await _unitOfWork.RuleRepo.GetByIdAsync(id);

                if (rule == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Không tìm thấy quy tắc để cập nhật."
                    };
                }

                rule.Name = dto.Name ?? rule.Name;
                rule.Description = dto.Description ?? rule.Description;
                rule.Category = dto.Category ?? rule.Category;
                rule.Value = dto.Value ?? rule.Value;
                rule.Unit = dto.Unit ?? rule.Unit;
                rule.AppliedScopeType = dto.AppliedScopeType ?? rule.AppliedScopeType;
                rule.AppliedScopeId = dto.AppliedScopeId ?? rule.AppliedScopeId;
                rule.EffectiveFrom = dto.EffectiveFrom ?? rule.EffectiveFrom;
                rule.EffectiveTo = dto.EffectiveTo ?? rule.EffectiveTo;
                rule.Status = dto.Status ?? rule.Status;

                await _unitOfWork.RuleRepo.UpdateAsync(rule);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Cập nhật quy tắc thành công.",
                    Result = rule
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = "Lỗi khi cập nhật quy tắc: " + ex.Message
                };
            }
        }
        public async Task<ResponseDTO> DeleteRuleAsync(Guid id)
        {
            try
            {
                var rule = await _unitOfWork.RuleRepo.GetByIdAsync(id);

                if (rule == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Không tìm thấy quy tắc để xóa."
                    };
                }

                _unitOfWork.RuleRepo.Delete(rule);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Xóa quy tắc thành công."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = "Lỗi khi xóa quy tắc: " + ex.Message
                };
            }
        }
    }
}







