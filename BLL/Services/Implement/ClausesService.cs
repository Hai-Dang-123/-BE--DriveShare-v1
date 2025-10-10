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
                ClauseContents = new List<ClauseContent>()
            };

            if (dto.ClauseContents != null && dto.ClauseContents.Any())
            {
                foreach (var c in dto.ClauseContents)
                {
                    clauseTemplate.ClauseContents.Add(new ClauseContent
                    {
                        ClauseContentId = Guid.NewGuid(),
                        Content = c.Content,
                        IsMandatory = c.IsMandatory,
                        DisplayOrder = c.DisplayOrder,
                        ClauseTemplate = clauseTemplate // Gắn reference cha
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
                Result = clauseTemplate
            };
        }

        

        public Task<ResponseDTO> DeleteClauseAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> GetAllClauseAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> GetClauseByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        //public Task<ResponseDTO> UpdateClauseAsync(UpdateClauseDTO updateClauseDTO)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

