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

        // UpdateContractTemplateAsync
        // DeleteContractTemplateAsync

        public async Task<ResponseDTO> CreateContractTemplateAsync(ContractTemplateDTO contractTemplateDTO)
        {
            try
            {
                
                var template = new ContractTemplate
                {
                    ContractTemplateId = Guid.NewGuid(),
                    Name = contractTemplateDTO.Name,
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
                        ContractTermId = Guid.NewGuid(),
                        ContractTemplateId = template.ContractTemplateId,
                        Content = t.Content,
                        IsMandatory = t.IsMandatory
                    }).ToList();

                    _unitOfWork.ContractTermRepo.AddRange(terms);
                    await _unitOfWork.SaveChangeAsync();
                }


                return new ResponseDTO
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "Contract template created successfully",
                    Result = new
                    {
                        template.ContractTemplateId,
                        template.Name,
                        template.Version,
                        Terms = contractTemplateDTO.Terms
                    }
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
        public async Task<ResponseDTO> GetAllContractTemplateasync()
        {
            try
            {
                var templates = await _unitOfWork.ContractTemplateRepo.GetAllWithTermsAsync();

                var result = templates.Select(t => new
                {
                    t.ContractTemplateId,
                    t.Name,
                    t.Version,
                    Terms = t.ContractTerms.Select(term => new
                    {
                        term.ContractTermId,
                        term.Content,
                        term.IsMandatory
                    })
                });

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Get all contract templates successfully",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Error retrieving contract templates: {ex.Message}"
                };
            }
        }
        public async Task<ResponseDTO> GetContractTemplateByIdAsync(Guid id)
        {
            try
            {
                var template = await _unitOfWork.ContractTemplateRepo.GetByIdWithTermsAsync(id);

                if (template == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Không tìm thấy mẫu hợp đồng."
                    };
                }

                // Map thủ công sang DTO
                var dto = new ContractTemplateResponseDTO
                {
                    ContractTemplateId = template.ContractTemplateId,
                    Name = template.Name,
                    Version = template.Version,
                    CreatedAt = template.CreatedAt,
                    Terms = template.ContractTerms?.Select(term => new ContractTermResponseDTO
                    {
                        ContractTemplateId = term.ContractTemplateId,
                        ContractTermId = term.ContractTermId,
                        IsMandatory = term.IsMandatory,
                        Content = term.Content
                    }).ToList() ?? new List<ContractTermResponseDTO>()
                };

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Lấy mẫu hợp đồng thành công.",
                    Result = dto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Đã xảy ra lỗi: {ex.Message}"
                };
            }
        }
        public async Task<ResponseDTO> UpdateContractTemplateAsync(Guid id, ContractTemplateDTO dto)
        {
            try
            {
                var existing = await _unitOfWork.ContractTemplateRepo.GetByIdWithTermsAsync(id);
                if (existing == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Không tìm thấy mẫu hợp đồng để cập nhật."
                    };
                }

                // ✅ Cập nhật thông tin cơ bản
                existing.Name = dto.Name;
                existing.Version = dto.Version;

                // ✅ Cập nhật điều khoản (xoá cũ - thêm mới)
                if (existing.ContractTerms != null && existing.ContractTerms.Any())
                {
                    _unitOfWork.ContractTermRepo.RemoveRange(existing.ContractTerms.ToList());
                }

                if (dto.Terms != null && dto.Terms.Any())
                {
                    var newTerms = dto.Terms.Select(t => new ContractTerm
                    {
                        ContractTermId = Guid.NewGuid(),
                        ContractTemplateId = existing.ContractTemplateId,
                        Content = t.Content,
                        IsMandatory = t.IsMandatory
                    }).ToList();

                    _unitOfWork.ContractTermRepo.AddRange(newTerms);
                }

                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Cập nhật mẫu hợp đồng thành công.",
                    Result = new
                    {
                        existing.ContractTemplateId,
                        existing.Name,
                        existing.Version,
                        Terms = dto.Terms
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = $"Lỗi khi cập nhật mẫu hợp đồng: {ex.Message}"
                };
            }
        }
        public async Task<ResponseDTO> DeleteContractTemplateAsync(Guid id)
        {
            try
            {
                var existing = await _unitOfWork.ContractTemplateRepo.GetByIdWithTermsAsync(id);
                if (existing == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Không tìm thấy mẫu hợp đồng để xoá."
                    };
                }

                // 🔹 Xoá điều khoản trước
                if (existing.ContractTerms != null && existing.ContractTerms.Any())
                {
                    _unitOfWork.ContractTermRepo.RemoveRange(existing.ContractTerms.ToList());
                }

                _unitOfWork.ContractTemplateRepo.Delete(existing);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Xoá mẫu hợp đồng thành công."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = $"Lỗi khi xoá mẫu hợp đồng: {ex.Message}"
                };
            }
        }

    }
}
