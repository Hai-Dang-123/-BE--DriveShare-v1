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

        // GET ALL
        public async Task<ResponseDTO> GetAllItemReportsAsync()
        {
            try
            {
                var reports = await _unitOfWork.ReportRepo.GetAll()
                    .OfType<ItemBookingReport>()
                    .Include(r => r.ItemBooking)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

                return new ResponseDTO("Lấy danh sách ItemBookingReport thành công.", 200, true, reports);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy danh sách báo cáo: {ex.Message}", 500, false);
            }
        }

        // GET BY ID
        public async Task<ResponseDTO> GetItemReportByIdAsync(Guid id)
        {
            try
            {
                var report = await _unitOfWork.ReportRepo.GetAll()
                    .OfType<ItemBookingReport>()
                    .Include(r => r.ItemBooking)
                    .FirstOrDefaultAsync(r => r.ReportId == id);

                if (report == null)
                    return new ResponseDTO("Không tìm thấy báo cáo.", 404, false);

                return new ResponseDTO("Lấy báo cáo thành công.", 200, true, report);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy báo cáo: {ex.Message}", 500, false);
            }
        }

        // CREATE
        public async Task<ResponseDTO> CreateItemReportAsync(CreateItemBookingReportDTO dto)
        {
            try
            {
                var itemBooking = await _unitOfWork.ItemBookingRepo.GetByIdAsync(dto.ItemBookingId);
                if (itemBooking == null)
                    return new ResponseDTO("Không tìm thấy ItemBooking tương ứng.", 404, false);

                var entity = new ItemBookingReport
                {
                    ReportId = Guid.NewGuid(),
                    ReportTitle = dto.ReportTitle,
                    ReportType = (ReportType)dto.ReportType,
                    Version = dto.Version,
                    Status = ReportStatus.PENDING,
                    ReportTemplateId = dto.ReportTemplateId,
                    ItemBookingId = dto.ItemBookingId,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.ItemBookingReportRepo.AddAsync(entity);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("Tạo ItemBookingReport thành công.", 201, true, entity);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi tạo báo cáo: {ex.Message}", 500, false);
            }
        }

        // UPDATE
        public async Task<ResponseDTO> UpdateItemReportAsync(Guid id, CreateItemBookingReportDTO dto)
        {
            try
            {
                var report = await _unitOfWork.ReportRepo.GetAll()
                    .OfType<ItemBookingReport>()
                    .FirstOrDefaultAsync(r => r.ReportId == id);

                if (report == null)
                    return new ResponseDTO("Không tìm thấy báo cáo để cập nhật.", 404, false);

                report.ReportTitle = dto.ReportTitle;
                report.ReportType = (ReportType)dto.ReportType;
                report.Version = dto.Version;
                report.ReportTemplateId = dto.ReportTemplateId;
                report.ItemBookingId = dto.ItemBookingId;

                await _unitOfWork.ItemBookingReportRepo.UpdateAsync(report);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("Cập nhật ItemBookingReport thành công.", 200, true, report);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi cập nhật báo cáo: {ex.Message}", 500, false);
            }
        }

        // DELETE
        public async Task<ResponseDTO> DeleteItemReportAsync(Guid id)
        {
            try
            {
                var report = await _unitOfWork.ReportRepo.GetAll()
                    .OfType<ItemBookingReport>()
                    .FirstOrDefaultAsync(r => r.ReportId == id);

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
