using BLL.Services.Interface;
using Common.DTOs;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BLL.Services.Implement
{
    public class ReportTemplateService : IReportTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ✅ CREATE
        public async Task<ResponseDTO> CreateReportTemplateAsync(CreateReportTemplateDTO dto)
        {
            try
            {
                var newTemplate = new ReportTemplate
                {
                    ReportTemplateId = Guid.NewGuid(),
                    Version = dto.Version,
                    CreatedAt = DateTime.UtcNow,
                    ReportTerms = dto.ReportTerms?.Select(termDto => new ReportTerm
                    {
                        ReportTermId = Guid.NewGuid(),
                        Content = termDto.Content,
                        IsMandatory = termDto.IsMandatory
                    }).ToList() ?? new List<ReportTerm>()
                };

                await _unitOfWork.ReportTemplateRepo.AddAsync(newTemplate);
                await _unitOfWork.SaveChangeAsync();

                var result = new ReportTemplateDTO
                {
                    ReportTemplateId = newTemplate.ReportTemplateId,
                    Version = newTemplate.Version,
                    CreatedAt = newTemplate.CreatedAt,
                    ReportTerms = newTemplate.ReportTerms.Select(t => new ReportTermDTO
                    {
                        ReportTermId = t.ReportTermId,
                        Content = t.Content,
                        IsMandatory = t.IsMandatory
                    }).ToList()
                };

                return new ResponseDTO("Tạo mẫu báo cáo thành công.", 201, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi tạo mẫu báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ UPDATE
        public async Task<ResponseDTO> UpdateReportTemplateAsync(Guid id, UpdateReportTemplateDTO dto)
        {
            try
            {
                var template = await _unitOfWork.ReportTemplateRepo.GetByIdWithTermsAsync(id);

                if (template == null)
                    return new ResponseDTO("Không tìm thấy mẫu báo cáo cần cập nhật.", 404, false);

                template.Version = dto.Version ?? template.Version;
                template.CreatedAt = DateTime.UtcNow;

                // Xóa toàn bộ term cũ rồi thêm lại
                template.ReportTerms.Clear();
                if (dto.ReportTerms != null && dto.ReportTerms.Any())
                {
                    template.ReportTerms = dto.ReportTerms.Select(termDto => new ReportTerm
                    {
                        ReportTermId = Guid.NewGuid(),
                        Content = termDto.Content,
                        IsMandatory = termDto.IsMandatory
                    }).ToList();
                }

                await _unitOfWork.ReportTemplateRepo.UpdateAsync(template);
                await _unitOfWork.SaveChangeAsync();

                var result = new ReportTemplateDTO
                {
                    ReportTemplateId = template.ReportTemplateId,
                    Version = template.Version,
                    CreatedAt = template.CreatedAt,
                    ReportTerms = template.ReportTerms.Select(t => new ReportTermDTO
                    {
                        ReportTermId = t.ReportTermId,
                        Content = t.Content,
                        IsMandatory = t.IsMandatory
                    }).ToList()
                };

                return new ResponseDTO("Cập nhật mẫu báo cáo thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi cập nhật mẫu báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ GET ALL
        public async Task<ResponseDTO> GetAllReportTemplatesAsync()
        {
            try
            {
                var templates = await _unitOfWork.ReportTemplateRepo
                    .GetAll()
                    .Include(t => t.ReportTerms)
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();

                if (templates == null || !templates.Any())
                    return new ResponseDTO("Không có mẫu báo cáo nào.", 404, false);

                var result = templates.Select(t => new ReportTemplateDTO
                {
                    ReportTemplateId = t.ReportTemplateId,
                    Version = t.Version,
                    CreatedAt = t.CreatedAt,
                    ReportTerms = t.ReportTerms.Select(term => new ReportTermDTO
                    {
                        ReportTermId = term.ReportTermId,
                        Content = term.Content,
                        IsMandatory = term.IsMandatory
                    }).ToList()
                }).ToList();

                return new ResponseDTO("Lấy danh sách mẫu báo cáo thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy danh sách mẫu báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ GET BY ID
        public async Task<ResponseDTO> GetReportTemplateByIdAsync(Guid id)
        {
            try
            {
                var template = await _unitOfWork.ReportTemplateRepo.GetByIdWithTermsAsync(id);

                if (template == null)
                    return new ResponseDTO("Không tìm thấy mẫu báo cáo.", 404, false);

                var result = new ReportTemplateDTO
                {
                    ReportTemplateId = template.ReportTemplateId,
                    Version = template.Version,
                    CreatedAt = template.CreatedAt,
                    ReportTerms = template.ReportTerms.Select(term => new ReportTermDTO
                    {
                        ReportTermId = term.ReportTermId,
                        Content = term.Content,
                        IsMandatory = term.IsMandatory
                    }).ToList()
                };

                return new ResponseDTO("Lấy mẫu báo cáo thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy mẫu báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ DELETE
        public async Task<ResponseDTO> DeleteReportTemplateAsync(Guid id)
        {
            try
            {
                var template = await _unitOfWork.ReportTemplateRepo.GetByIdWithTermsAsync(id);
                if (template == null)
                    return new ResponseDTO("Không tìm thấy mẫu báo cáo để xoá.", 404, false);

                _unitOfWork.ReportTermRepo.RemoveRange(template.ReportTerms.ToList());
                await _unitOfWork.ReportTemplateRepo.DeleteAsync(id);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("Xoá mẫu báo cáo thành công.", 200, true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi xoá mẫu báo cáo: {ex.Message}", 500, false);
            }
        }
    }
}
