using BLL.Services.Interface;
using Common.DTOs;
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
    public class ContractTermService : IContractTermService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ContractTermService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDTO> CreateContractTermsAsync(ContracttermDTO contractTermsDTO)
        {
            try
            {
                var contractTerm = new ContractTerm
                {
                    ContractTermId = Guid.NewGuid(),
                    ContractTemplateId = contractTermsDTO.ContractTemplateId,
                    Content = contractTermsDTO.Content,
                    IsMandatory = contractTermsDTO.IsMandatory
                };
                await _unitOfWork.ContractTermRepo.AddAsync(contractTerm);
                await _unitOfWork.SaveChangeAsync();
                var contractTermResponse = new ContracttermResponseDTO
                {
                    ContractTemplateId = contractTerm.ContractTemplateId,
                    IsMandatory = contractTerm.IsMandatory,
                    Content = contractTerm.Content
                };
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Contract term created successfully",
                    Result = contractTermResponse
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Error creating contract term: {ex.Message}",
                    Result = null
                };

            }
        }
        public  async Task<ResponseDTO> DeleteContractTermsAsync(Guid ContractTermid)
        {
            try
            {
                var contractTerm = await  _unitOfWork.ContractTermRepo.GetByIdAsync(ContractTermid);
                if (contractTerm == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Contract term not found",
                        Result = null
                    };
                }
                _unitOfWork.ContractTermRepo.Delete(contractTerm);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Contract term deleted successfully",
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Error deleting contract term: {ex.Message}",
                    Result = null
                };

            }
        }
        public async Task<ResponseDTO> GetAllContractTermsAsync()
        {
            try
            {
                var contractTerms = await _unitOfWork.ContractTermRepo.GetContractTerms();
                var contractTermDTOs = contractTerms.Select(ct => new ContracttermResponseDTO
                {
                    ContractTemplateId = ct.ContractTemplateId,
                    IsMandatory = ct.IsMandatory,
                    Content = ct.Content
                }).ToList();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Contract terms retrieved successfully",
                    Result = contractTermDTOs
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Error retrieving contract terms: {ex.Message}",
                    Result = null
                };
            }
            }
        public async Task<ResponseDTO> GetContractTermsAsync(Guid ContractTermid)
        {
            try
            {
                var contractTerm = await _unitOfWork.ContractTermRepo.GetByIdAsync(ContractTermid);
                if (contractTerm == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Contract term not found",
                        Result = null
                    };
                }
                var responseDTO = new ContracttermResponseDTO
                {
                    ContractTemplateId = contractTerm.ContractTemplateId,
                    IsMandatory = contractTerm.IsMandatory,
                    Content = contractTerm.Content
                };
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Contract term retrieved successfully",
                    Result = responseDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error retrieving contract term: {ex.Message}",                  
                };
            }

        }
        public async Task<ResponseDTO> UpdateContractTermsAsync(UpdateContracttermDTO contractTermsDTO)
        {
            try
            {
                var contractTerm = await _unitOfWork.ContractTermRepo.GetByIdAsync(contractTermsDTO.ContractTermId);
                if (contractTerm == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Contract term not found",
                    };
                }
                contractTerm.Content = contractTermsDTO.Content;
                contractTerm.IsMandatory = contractTermsDTO.IsMandatory;
                contractTerm.ContractTemplateId = contractTermsDTO.ContractTemplateId;
                _unitOfWork.ContractTermRepo.UpdateAsync(contractTerm);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Contract term updated successfully",
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error updating contract term: {ex.Message}",
                };
            }
        }
    }
}
