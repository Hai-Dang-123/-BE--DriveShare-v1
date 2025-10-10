using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTOs;
using Common.Enums;
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
    public class ContractTemplateService : IContractTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        public ContractTemplateService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }
        public async Task<ResponseDTO> CreateContractTemplateAsync(ContractTemplateDTO contractTemplateDTO)
        {
            try
            {
                
                var template = new ContractTemplate
                {
                    ContractTemplateId = Guid.NewGuid(),
                    Version = contractTemplateDTO.Version
                };

                //_context.ContractTemplates.Add(template);
                await _unitOfWork.ContractTemplateRepo.AddAsync(template);
                await _unitOfWork.SaveChangeAsync();

                // 2️⃣ Thêm ContractTerms nếu có
                if (contractTemplateDTO.Terms != null && contractTemplateDTO.Terms.Any())
                {
                    var terms = contractTemplateDTO.Terms.Select(t => new ContractTerm
                    {
                        ContractTemplateId = template.ContractTemplateId,
                        
                        Content = t.Content,
                        IsMandatory = true
                    }).ToList();

                    _unitOfWork.ContractTermRepo.AddRange(terms);
                    await _unitOfWork.SaveChangeAsync();
                }


                return new ResponseDTO
                {
                    IsSuccess = true,
                    Message = "Contract template created successfully",
                    Result = template
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Error creating contract template: {ex.Message}"
                };
            }
        }
    }
}
