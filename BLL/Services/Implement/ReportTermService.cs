using BLL.Services.Interface;
using Common.DTOs;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ReportTermService : IReportTermService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportTermService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ✅ CREATE
        public async Task<ResponseDTO> CreateReportTermAsync(CreateReportTermDTO dto)
        {
            try
            {
                if (dto == null)
                    return new ResponseDTO("Dữ liệu đầu vào không hợp lệ.", 400, false);

                var entity = new ReportTerm
                {
                    ReportTermId = Guid.NewGuid(),
                    Content = dto.Content,
                    IsMandatory = dto.IsMandatory
                };

                await _unitOfWork.ReportTermRepo.AddAsync(entity);
                await _unitOfWork.SaveChangeAsync();

                var result = new ReportTermDTO
                {
                    ReportTermId = entity.ReportTermId,
                    Content = entity.Content,
                    IsMandatory = entity.IsMandatory
                };

                return new ResponseDTO("Tạo điều khoản báo cáo thành công.", 201, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi tạo điều khoản báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ UPDATE
        public async Task<ResponseDTO> UpdateReportTermAsync(Guid id, CreateReportTermDTO dto)
        {
            try
            {
                var existing = await _unitOfWork.ReportTermRepo.GetByIdAsync(id);
                if (existing == null)
                    return new ResponseDTO("Không tìm thấy điều khoản báo cáo.", 404, false);

                existing.Content = dto.Content;
                existing.IsMandatory = dto.IsMandatory;

                await _unitOfWork.ReportTermRepo.UpdateAsync(existing);
                await _unitOfWork.SaveChangeAsync();

                var result = new ReportTermDTO
                {
                    ReportTermId = existing.ReportTermId,
                    Content = existing.Content,
                    IsMandatory = existing.IsMandatory
                };

                return new ResponseDTO("Cập nhật điều khoản báo cáo thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi cập nhật điều khoản báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ GET ALL
        public async Task<ResponseDTO> GetAllReportTermsAsync()
        {
            try
            {
                var terms = await _unitOfWork.ReportTermRepo.GetAll().ToListAsync();

                if (!terms.Any())
                    return new ResponseDTO("Không có điều khoản báo cáo nào.", 404, false);

                var result = terms.Select(t => new ReportTermDTO
                {
                    ReportTermId = t.ReportTermId,
                    Content = t.Content,
                    IsMandatory = t.IsMandatory
                }).ToList();

                return new ResponseDTO("Lấy danh sách điều khoản báo cáo thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy điều khoản báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ GET BY ID
        public async Task<ResponseDTO> GetReportTermByIdAsync(Guid id)
        {
            try
            {
                var term = await _unitOfWork.ReportTermRepo.GetByIdAsync(id);
                if (term == null)
                    return new ResponseDTO("Không tìm thấy điều khoản báo cáo.", 404, false);

                var result = new ReportTermDTO
                {
                    ReportTermId = term.ReportTermId,
                    Content = term.Content,
                    IsMandatory = term.IsMandatory
                };

                return new ResponseDTO("Lấy điều khoản báo cáo thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy điều khoản báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ DELETE
        public async Task<ResponseDTO> DeleteReportTermAsync(Guid id)
        {
            try
            {
                var term = await _unitOfWork.ReportTermRepo.GetByIdAsync(id);
                if (term == null)
                    return new ResponseDTO("Không tìm thấy điều khoản để xoá.", 404, false);

                _unitOfWork.ReportTermRepo.Delete(term);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("Xoá điều khoản báo cáo thành công.", 200, true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi xoá điều khoản báo cáo: {ex.Message}", 500, false);
            }
        }
    }
}
