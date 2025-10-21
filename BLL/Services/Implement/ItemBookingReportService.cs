using BLL.Services.Interface;
using Common.DTOs;
using Common.Enums;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ItemBookingReportService : IItemBookingReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemBookingReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ✅ GET ALL
        public async Task<ResponseDTO> GetAllItemReportsAsync()
        {
            try
            {
                var reports = await _unitOfWork.ItemBookingReportRepo
                    .GetAll()
                    .Include(r => r.ItemBooking)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

                if (!reports.Any())
                    return new ResponseDTO("Không có báo cáo nào.", 404, false);

                var result = reports.Select(r => new ItemBookingReportDTO
                {
                    ReportId = r.ReportId,
                    ReportTitle = r.ReportTitle,
                    Version = r.Version,
                    ReportType = r.ReportType,
                    Status = r.Status,
                    OwnerSigned = r.OwnerSigned,
                    RenterSigned = r.RenterSigned,
                    CreatedAt = r.CreatedAt,
                    SignedAt = r.SignedAt,
                    ReportTemplateId = r.ReportTemplateId,
                    ItemBookingId = r.ItemBookingId
                }).ToList();

                return new ResponseDTO("Lấy danh sách ItemBookingReport thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy danh sách báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ GET BY ID
        public async Task<ResponseDTO> GetItemReportByIdAsync(Guid id)
        {
            try
            {
                var report = await _unitOfWork.ItemBookingReportRepo.GetAll()
                    .Include(r => r.ItemBooking)
                    .FirstOrDefaultAsync(r => r.ReportId == id);

                if (report == null)
                    return new ResponseDTO("Không tìm thấy báo cáo.", 404, false);

                var dto = new ItemBookingReportDTO
                {
                    ReportId = report.ReportId,
                    ReportTitle = report.ReportTitle,
                    Version = report.Version,
                    ReportType = report.ReportType,
                    Status = report.Status,
                    OwnerSigned = report.OwnerSigned,
                    RenterSigned = report.RenterSigned,
                    CreatedAt = report.CreatedAt,
                    SignedAt = report.SignedAt,
                    ReportTemplateId = report.ReportTemplateId,
                    ItemBookingId = report.ItemBookingId
                };

                return new ResponseDTO("Lấy báo cáo thành công.", 200, true, dto);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ CREATE
        public async Task<ResponseDTO> CreateItemReportAsync(CreateItemBookingReportDTO dto)
        {
            try
            {
                if (dto.ItemBookingId == Guid.Empty)
                    return new ResponseDTO("ItemBookingId không hợp lệ.", 400, false);

                var itemBooking = await _unitOfWork.ItemBookingRepo.GetByIdAsync(dto.ItemBookingId);
                if (itemBooking == null)
                    return new ResponseDTO("Không tìm thấy ItemBooking tương ứng.", 404, false);

                var entity = new ItemBookingReport
                {
                    ReportId = Guid.NewGuid(),
                    ReportTitle = dto.ReportTitle,
                    ReportType = dto.ReportType,
                    Version = dto.Version,
                    Status = ReportStatus.PENDING,
                    OwnerSigned = false,
                    RenterSigned = false,
                    // ✅ Fix: Guid? -> Guid (nếu null thì gán Guid.Empty)
                    ReportTemplateId = dto.ReportTemplateId ?? Guid.Empty,
                    ItemBookingId = dto.ItemBookingId,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.ItemBookingReportRepo.AddAsync(entity);
                await _unitOfWork.SaveChangeAsync();

                var result = new ItemBookingReportDTO
                {
                    ReportId = entity.ReportId,
                    ReportTitle = entity.ReportTitle,
                    Version = entity.Version,
                    ReportType = entity.ReportType,
                    Status = entity.Status,
                    OwnerSigned = entity.OwnerSigned,
                    RenterSigned = entity.RenterSigned,
                    CreatedAt = entity.CreatedAt,
                    SignedAt = entity.SignedAt,
                    ReportTemplateId = entity.ReportTemplateId,
                    ItemBookingId = entity.ItemBookingId
                };

                return new ResponseDTO("Tạo ItemBookingReport thành công.", 201, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi tạo báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ UPDATE
        public async Task<ResponseDTO> UpdateItemReportAsync(Guid id, CreateItemBookingReportDTO dto)
        {
            try
            {
                var report = await _unitOfWork.ItemBookingReportRepo.GetByIdAsync(id);
                if (report == null)
                    return new ResponseDTO("Không tìm thấy báo cáo để cập nhật.", 404, false);

                report.ReportTitle = dto.ReportTitle;
                report.ReportType = dto.ReportType;
                report.Version = dto.Version;
                // ✅ Fix: Guid? -> Guid
                report.ReportTemplateId = dto.ReportTemplateId ?? Guid.Empty;
                report.ItemBookingId = dto.ItemBookingId;

                await _unitOfWork.ItemBookingReportRepo.UpdateAsync(report);
                await _unitOfWork.SaveChangeAsync();

                var result = new ItemBookingReportDTO
                {
                    ReportId = report.ReportId,
                    ReportTitle = report.ReportTitle,
                    Version = report.Version,
                    ReportType = report.ReportType,
                    Status = report.Status,
                    OwnerSigned = report.OwnerSigned,
                    RenterSigned = report.RenterSigned,
                    CreatedAt = report.CreatedAt,
                    SignedAt = report.SignedAt,
                    ReportTemplateId = report.ReportTemplateId,
                    ItemBookingId = report.ItemBookingId
                };

                return new ResponseDTO("Cập nhật ItemBookingReport thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi cập nhật báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ DELETE
        public async Task<ResponseDTO> DeleteItemReportAsync(Guid id)
        {
            try
            {
                var report = await _unitOfWork.ItemBookingReportRepo.GetByIdAsync(id);
                if (report == null)
                    return new ResponseDTO("Không tìm thấy báo cáo để xoá.", 404, false);

                _unitOfWork.ItemBookingReportRepo.Delete(report);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("Xoá ItemBookingReport thành công.", 200, true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi xoá báo cáo: {ex.Message}", 500, false);
            }
        }
    }
}
