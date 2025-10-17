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
    public class ClauseTermServices : IClauseTermServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClauseTermServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreateClauseTermAync(ClauseTermDTO clauseTermDTO)
        {
            try
            {
                var clauseTerm = new ClauseTerm
                {
                    ClauseTermId = Guid.NewGuid(),
                    ClauseTemplateId = clauseTermDTO.ClauseTemplateId,
                    Content = clauseTermDTO.Content,
                    IsMandatory = clauseTermDTO.IsMandatory,
                    DisplayOrder = clauseTermDTO.DisplayOrder
                };
                _unitOfWork.ClauseTermRepo.AddAsync(clauseTerm);
                await _unitOfWork.SaveChangeAsync();
                var responseClauseTermDTO = new ResponseClauseTermDTO
                {
                    ClauseTermId = clauseTerm.ClauseTermId,
                    ClauseTemplateId = clauseTerm.ClauseTemplateId,
                    Content = clauseTerm.Content,
                    IsMandatory = clauseTerm.IsMandatory,
                    DisplayOrder = clauseTerm.DisplayOrder
                };
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Clause term created successfully.",
                    Result = responseClauseTermDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = $"An error occurred while creating the clause term: {ex.Message}"
                };
            }

        }

        public async Task<ResponseDTO> DeleteClauseTermAsync(Guid ClauseTermId)
        {
            try
            {
                var clauseTerm = await _unitOfWork.ClauseTermRepo.GetByIdAsync(ClauseTermId);
                if (clauseTerm == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Clause term not found."
                    };
                }
                _unitOfWork.ClauseTermRepo.Delete(clauseTerm);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Clause term deleted successfully."
                };

            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while deleting the clause term: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO> GetAllClauseTermsAsync()
        {
            try
            {
                var clauseTerms = await _unitOfWork.ClauseTermRepo.GetAllClauseTerm();
                var responseClauseTerms = clauseTerms.Select(ct => new ResponseClauseTermDTO
                {
                    ClauseTermId = ct.ClauseTermId,
                    ClauseTemplateId = ct.ClauseTemplateId,
                    Content = ct.Content,
                    IsMandatory = ct.IsMandatory,
                    DisplayOrder = ct.DisplayOrder
                }).ToList();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Clause terms retrieved successfully.",
                    Result = responseClauseTerms
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while retrieving clause terms: {ex.Message}"
                };
            }

        }

        public async Task<ResponseDTO> GetClauseTermByIdAsync(Guid ClauseTermId)
        {
            try
            {
                var clauseTerm = await _unitOfWork.ClauseTermRepo.GetByIdAsync(ClauseTermId);
                if (clauseTerm == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Clause term not found."
                    };
                }
                var responseClauseTermDTO = new ResponseClauseTermDTO
                {
                    ClauseTermId = clauseTerm.ClauseTermId,
                    ClauseTemplateId = clauseTerm.ClauseTemplateId,
                    Content = clauseTerm.Content,
                    IsMandatory = clauseTerm.IsMandatory,
                    DisplayOrder = clauseTerm.DisplayOrder
                };
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Clause term retrieved successfully.",
                    Result = responseClauseTermDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while retrieving the clause term: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO> UpdateClauseTermAsync(UpdateClauseTermDTO clauseTermDTO)
        {
            try
            {
                var clauseTerm = await _unitOfWork.ClauseTermRepo.GetByIdAsync(clauseTermDTO.ClauseTermId);
                if (clauseTerm == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Clause term not found."
                    };
                }
                clauseTerm.Content = clauseTermDTO.Content;
                clauseTerm.IsMandatory = clauseTermDTO.IsMandatory;
                clauseTerm.DisplayOrder = clauseTermDTO.DisplayOrder;
                _unitOfWork.ClauseTermRepo.UpdateAsync(clauseTerm);
                await _unitOfWork.SaveChangeAsync();
                var responseClauseTermDTO = new ResponseClauseTermDTO
                {
                    ClauseTermId = clauseTerm.ClauseTermId,
                    ClauseTemplateId = clauseTerm.ClauseTemplateId,
                    Content = clauseTerm.Content,
                    IsMandatory = clauseTerm.IsMandatory,
                    DisplayOrder = clauseTerm.DisplayOrder
                };
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Clause term updated successfully.",
                    Result = responseClauseTermDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while updating the clause term: {ex.Message}"
                };
            }
        }
    }
}
