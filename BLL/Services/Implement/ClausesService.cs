//using BLL.Services.Interface;
//using BLL.Utilities;
using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTOs;
using Common.Messages;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ClausesService : IClausesTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;

        public ClausesService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }

        public async Task<ResponseDTO> CreateClauseAsync(CreateClauseTemplateDTO dto)
        {
            var clauseTemplate = new ClauseTemplate
            {
                ClauseId = Guid.NewGuid(),
                Version = dto.Version,
                Title = dto.Title,
                Terms = new List<ClauseTerm>()
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

        public async Task<ResponseDTO> GetAllClausesAsync()
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

        public async Task<ResponseDTO> GetClauseByIdAsync(Guid clauseId)
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

        public async Task<ResponseDTO> UpdateClauseAsync(UpdateClauseTemplateDTO dto)
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

