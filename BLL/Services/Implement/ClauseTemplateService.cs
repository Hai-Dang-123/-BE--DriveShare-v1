using BLL.Services.Interface;
using Common.DTOs;
using Common.Enums;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ClauseTemplateService : IClausesTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClauseTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreateClauseTemplateAsync(CreateClauseTemplateDTO dto)
        {
            var clauseTemplate = new ClauseTemplate
            {
                ClauseId = Guid.NewGuid(),
                Version = dto.Version,
                Title = dto.Title,
                Terms = new List<ClauseTerm>(),
                Status = ClauseTemplateStatus.ACTIVE

            };

            if (dto.ClauseContents != null && dto.ClauseContents.Any())
            {
                foreach (var c in dto.ClauseContents)
                {
                    clauseTemplate.Terms.Add(new ClauseTerm
                    {
                        ClauseTermId = Guid.NewGuid(),
                        ClauseTemplateId = clauseTemplate.ClauseId,
                        Content = c.Content,
                        IsMandatory = c.IsMandatory,
                        DisplayOrder = c.DisplayOrder
                    });
                }
            }

            await _unitOfWork.ClauseTemplateRepo.AddAsync(clauseTemplate);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                StatusCode = 201,
                Message = "Tạo mẫu điều khoản thành công",
                Result = new
                {
                    clauseTemplate.ClauseId,
                    clauseTemplate.Title,
                    clauseTemplate.Version,
                    Terms = clauseTemplate.Terms.Select(t => new
                    {
                        t.ClauseTermId,
                        t.Content,
                        t.IsMandatory,
                        t.DisplayOrder
                    })
                }
            };
        }

        public async Task<ResponseDTO> DeleteClauseTemplateAsync(Guid clauseId)
        {
            try
            {
                var clauseTemplate = await _unitOfWork.ClauseTemplateRepo.GetByIdAsync(clauseId);
                if (clauseTemplate == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy mẫu điều khoản"
                    };
                }
                clauseTemplate.Status = ClauseTemplateStatus.DELETED;
                await _unitOfWork.ClauseTemplateRepo.UpdateAsync(clauseTemplate);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Xóa mẫu điều khoản thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Lỗi khi xóa mẫu điều khoản: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO> GetAllClausesTemplateAsync()
        {

            var clauseTemplates = await _unitOfWork.ClauseTemplateRepo.GetAllWithTermsAsync();

            var result = clauseTemplates.Select(ct => new 
            {
                ct.ClauseId,
                ct.Title,
                ct.Version,
                Terms = ct.Terms.Select(t => new
                {
                    t.ClauseTermId,
                    t.Content,
                    t.IsMandatory,
                    t.DisplayOrder
                })
            }).ToList();

            return new ResponseDTO
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Lấy danh sách mẫu điều khoản thành công",
                Result = result
            };
        }

        public async Task<ResponseDTO> GetClauseTemplateByIdAsync(Guid clauseId)
        {
            var clauseTemplate = await _unitOfWork.ClauseTemplateRepo.GetClauseWithTermsAsync(clauseId);

            if (clauseTemplate == null)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    Message = "Không tìm thấy mẫu điều khoản"
                };
            }

            var result = new
            {
                clauseTemplate.ClauseId,
                clauseTemplate.Title,
                clauseTemplate.Version,
                Terms = clauseTemplate.Terms.Select(t => new
                {
                    t.ClauseTermId,
                    t.Content,
                    t.IsMandatory,
                    t.DisplayOrder
                })
            };

            return new ResponseDTO
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Lấy chi tiết mẫu điều khoản thành công",
                Result = result
            };
        }

        public async Task<ResponseDTO> UpdateClauseTemplateAsync(UpdateClauseTemplateDTO dto)
        {
            var clauseTemplate = await _unitOfWork.ClauseTemplateRepo
         .GetClauseWithTermsAsync(dto.ClauseId);

            if (clauseTemplate == null)
                return new ResponseDTO { IsSuccess = false, StatusCode = 404, Message = "Không tìm thấy mẫu điều khoản" };

            // Update logic
            clauseTemplate.Title = dto.Title;
            clauseTemplate.Version = dto.Version;

            // Update, add, delete terms
            var oldTerms = clauseTemplate.Terms.ToList();
            foreach (var term in oldTerms)
            {
                if (!dto.ClauseContents.Any(c => c.ClauseTermId == term.ClauseTermId))
                    _unitOfWork.ClauseTermRepo.Delete(term);
            }

            foreach (var c in dto.ClauseContents)
            {
                var existingTerm = clauseTemplate.Terms.FirstOrDefault(t => t.ClauseTermId == c.ClauseTermId);
                if (existingTerm != null)
                {
                    existingTerm.Content = c.Content;
                    existingTerm.IsMandatory = c.IsMandatory;
                    existingTerm.DisplayOrder = c.DisplayOrder;
                }
                else
                {
                    clauseTemplate.Terms.Add(new ClauseTerm
                    {
                        ClauseTermId = Guid.NewGuid(),
                        Content = c.Content,
                        IsMandatory = c.IsMandatory,
                        DisplayOrder = c.DisplayOrder,
                        ClauseTemplateId = clauseTemplate.ClauseId
                    });
                }
            }

            await _unitOfWork.SaveAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Cập nhật mẫu điều khoản thành công",
                Result = clauseTemplate
            };
        }

        
    }
}
