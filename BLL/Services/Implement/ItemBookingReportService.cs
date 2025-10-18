using BLL.Services.Interface;
using Common.DTOs;
using Common.Enums;
using Common.Messages;
using DAL.Entities;
using DAL.UnitOfWork;
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

        public async Task<ResponseDTO> CreateReportAsync(CreateItemBookingReportDTO dto)
        {
            var itemBooking = await _unitOfWork.ItemBookingRepo.GetByIdAsync(dto.ItemBookingId);
            if (itemBooking == null)
            {
                return new ResponseDTO("ItemBooking không tồn tại.", 404, false);
            }

            var newReport = new ItemBookingReport
            {
                ReportId = Guid.NewGuid(),
                ItemBookingId = dto.ItemBookingId,
                ReportTitle = dto.ReportTitle,
                ReportType = dto.ReportType,
                Version = dto.Version,
                CreatedAt = DateTime.UtcNow,
                Status = ReportStatus.PENDING
            };

            try
            {
                await _unitOfWork.ItemBookingReportRepo.AddAsync(newReport);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                return new ResponseDTO("Đã xảy ra lỗi khi tạo báo cáo.", 500, false);
            }

            return new ResponseDTO("Tạo báo cáo thành công.", 201, true);
        }

        public async Task<ResponseDTO> GetAllReportsAsync()
        {
            var reports =  _unitOfWork.ItemBookingReportRepo.GetAll();
            if (!reports.Any())
            {
                return new ResponseDTO("Không có báo cáo nào.", 404, false);
            }

            return new ResponseDTO("Lấy danh sách báo cáo thành công.", 200, true, reports);
        }

        public async Task<ResponseDTO> GetReportByIdAsync(Guid id)
        {
            var report = await _unitOfWork.ItemBookingReportRepo.GetByIdAsync(id);
            if (report == null)
            {
                return new ResponseDTO("Báo cáo không tồn tại.", 404, false);
            }

            return new ResponseDTO("Lấy báo cáo thành công.", 200, true, report);
        }

        public async Task<ResponseDTO> UpdateReportAsync(Guid id, CreateItemBookingReportDTO dto)
        {
            var report = await _unitOfWork.ItemBookingReportRepo.GetByIdAsync(id);
            if (report == null)
            {
                return new ResponseDTO("Báo cáo không tồn tại.", 404, false);
            }

            report.ReportTitle = dto.ReportTitle;
            report.ReportType = dto.ReportType;
            report.Version = dto.Version;

            try
            {
                await _unitOfWork.ItemBookingReportRepo.UpdateAsync(report);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                return new ResponseDTO("Đã xảy ra lỗi khi cập nhật báo cáo.", 500, false);
            }

            return new ResponseDTO("Cập nhật báo cáo thành công.", 200, true);
        }

        public async Task<ResponseDTO> DeleteReportAsync(Guid id)
        {
            var report = await _unitOfWork.ItemBookingReportRepo.GetByIdAsync(id);
            if (report == null)
            {
                return new ResponseDTO("Báo cáo không tồn tại.", 404, false);
            }

            try
            {
                await _unitOfWork.ItemBookingReportRepo.DeleteAsync(id);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                return new ResponseDTO("Đã xảy ra lỗi khi xóa báo cáo.", 500, false);
            }

            return new ResponseDTO("Xóa báo cáo thành công.", 200, true);
        }
    }
}