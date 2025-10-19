using BLL.Services.Interface;
using Common.DTOs;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ReportTemplateService : IReportTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ResponseDTO> CreateReportTemplateAsync(CreateReportTemplateDTO dto)
        {
            try
            {
                var newTemplate = new DAL.Entities.ReportTemplate
                {
                    ReportTemplateId = Guid.NewGuid(),
                    Version = dto.Version,
                    CreatedAt = DateTime.UtcNow,
                };

                // Nếu có term thì thêm vào
                if (dto.ReportTerms != null && dto.ReportTerms.Any())
                {
                    foreach (var termDto in dto.ReportTerms)
                    {
                        newTemplate.ReportTerms.Add(new DAL.Entities.ReportTerm
                        {
                            ReportTermId = Guid.NewGuid(),
                            Content = termDto.Content,
                            IsMandatory = termDto.IsMandatory
                        });
                    }
                }

                await _unitOfWork.ReportTemplateRepo.AddAsync(newTemplate);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "Tạo mẫu báo cáo thành công.",
                    Result = newTemplate
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Lỗi khi tạo mẫu báo cáo: {ex.Message}"
                };
            }
        }
        public async Task<ResponseDTO> UpdateReportTemplateAsync(Guid id, UpdateReportTemplateDTO dto)
        {
            try
            {
                var template = await _unitOfWork.ReportTemplateRepo
                    .GetByIdWithTermsAsync(id);

                if (template == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Không tìm thấy mẫu báo cáo cần cập nhật."
                    };
                }

                template.Version = dto.Version ?? template.Version;
                template.CreatedAt = DateTime.UtcNow;

                // Cập nhật lại các term (xóa cũ, thêm mới)
                if (template.ReportTerms.Any())
                {
                    _unitOfWork.ReportTermRepo.RemoveRange(template.ReportTerms.ToList());
                }

                if (dto.ReportTerms != null && dto.ReportTerms.Any())
                {
                    foreach (var termDto in dto.ReportTerms)
                    {
                        template.ReportTerms.Add(new DAL.Entities.ReportTerm
                        {
                            ReportTermId = Guid.NewGuid(),
                            Content = termDto.Content,
                            IsMandatory = termDto.IsMandatory
                        });
                    }
                }

                await _unitOfWork.ReportTemplateRepo.UpdateAsync(template);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Cập nhật mẫu báo cáo thành công.",
                    Result = template
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Lỗi khi cập nhật mẫu báo cáo: {ex.Message}"
                };
            }
        }
        public async Task<ResponseDTO> GetAllReportTemplatesAsync()
        {
            try
            {
                var templates = await _unitOfWork.ReportTemplateRepo.GetAll()
                    .Include(t => t.ReportTerms)
                    .ToListAsync();

                if (templates == null || !templates.Any())
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Không có mẫu báo cáo nào."
                    };
                }

                var result = templates.Select(t => new
                {
                    t.ReportTemplateId,
                    t.Version,
                    t.CreatedAt,
                    Terms = t.ReportTerms.Select(term => new
                    {
                        term.ReportTermId,
                        term.Content,
                        term.IsMandatory
                    })
                });

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Lấy danh sách mẫu báo cáo thành công.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Lỗi khi lấy danh sách mẫu báo cáo: {ex.Message}"
                };
            }
        }
        public async Task<ResponseDTO> GetReportTemplateByIdAsync(Guid id)
        {
            try
            {
                var template = await _unitOfWork.ReportTemplateRepo.GetByIdWithTermsAsync(id);

                if (template == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Không tìm thấy mẫu báo cáo."
                    };
                }

                var result = new
                {
                    template.ReportTemplateId,
                    template.Version,
                    template.CreatedAt,
                    Terms = template.ReportTerms.Select(term => new
                    {
                        term.ReportTermId,
                        term.Content,
                        term.IsMandatory
                    })
                };

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Lấy mẫu báo cáo thành công.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Lỗi khi lấy mẫu báo cáo: {ex.Message}"
                };
            }
        }
        public async Task<ResponseDTO> DeleteReportTemplateAsync(Guid id)
        {
            try
            {
                var template = await _unitOfWork.ReportTemplateRepo.GetByIdAsync(id);
                if (template == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Report template not found."
                    };
                }

                if (template.ReportTerms != null && template.ReportTerms.Count > 0)
                {
                    _unitOfWork.ReportTermRepo.RemoveRange(template.ReportTerms.ToList());
                }

                await _unitOfWork.ReportTemplateRepo.DeleteAsync(id);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Report template deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Error deleting report template: {ex.Message}"
                };
            }
        }
    }
}
