using BLL.Services.Interface;
using Common.DTOs;
using Common.Enums;
using Common.Messages;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class VehicleBookingReportService : IVehicleBookingReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleBookingReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDTO> GetAllVehicleBookingReportsAsync()
        {
            try
            {
                var reports = await _unitOfWork.VehicleBookingReportRepo.GetAll()
                    .Include(r => r.VehicleBooking)
                        .ThenInclude(b => b.PostVehicle)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

                return new ResponseDTO("Lấy danh sách VehicleBookingReport thành công.", 200, true, reports);
            }
            catch (Exception ex)
        {
                return new ResponseDTO($"Lỗi khi lấy danh sách báo cáo: {ex.Message}", 500, false);
            }
        }

        // ==========================================================
        // 🔹 GET BY ID
        // ==========================================================
        public async Task<ResponseDTO> GetVehicleBookingReportByIdAsync(Guid id)
            {
            try
            {
                var report = await _unitOfWork.VehicleBookingReportRepo.GetAll()
                    .Include(r => r.VehicleBooking)
                        .ThenInclude(b => b.PostVehicle)
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
        // ==========================================================
        // 🔹 CREATE
        // ==========================================================
        public async Task<ResponseDTO> CreateVehicleBookingReportAsync(CreateVehicleBookingReportDTO dto)
        {
            try
            {
                var vehicleBooking = await _unitOfWork.VehicleBookingRepo.GetByIdAsync(dto.VehicleBookingId);
                if (vehicleBooking == null)
                    return new ResponseDTO("Không tìm thấy VehicleBooking tương ứng.", 404, false);

                var entity = new VehicleBookingReport
            {
                ReportId = Guid.NewGuid(),
                VehicleBookingId = dto.VehicleBookingId,
                ReportTitle = dto.ReportTitle,
                    ReportType = (ReportType)dto.ReportType,
                Version = dto.Version,
                    Status = ReportStatus.PENDING,
                    ReportTemplateId = dto.ReportTemplateId,
                    CreatedAt = DateTime.UtcNow
            };

                await _unitOfWork.VehicleBookingReportRepo.AddAsync(entity);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                return new ResponseDTO("Đã xảy ra lỗi khi tạo báo cáo.", 500, false);
            }

                return new ResponseDTO("Tạo VehicleBookingReport thành công.", 201, true);
        }

        // ==========================================================
        // 🔹 UPDATE
        // ==========================================================


        public async Task<ResponseDTO> UpdateVehicleBookingReportAsync(Guid id, CreateVehicleBookingReportDTO dto)
        {
            var report = await _unitOfWork.VehicleBookingReportRepo.GetByIdAsync(id);
            if (report == null)
                    return new ResponseDTO("Không tìm thấy báo cáo để cập nhật.", 404, false);

            report.ReportTitle = dto.ReportTitle;
                report.ReportType = (ReportType)dto.ReportType;
            report.Version = dto.Version;
                report.ReportTemplateId = dto.ReportTemplateId;
                report.VehicleBookingId = dto.VehicleBookingId;

            try
            {
                await _unitOfWork.VehicleBookingReportRepo.UpdateAsync(report);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("Cập nhật VehicleBookingReport thành công.", 200, true, report);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi cập nhật báo cáo: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Cập nhật báo cáo thành công.", 200, true);
        }
        // ==========================================================
        // 🔹 DELETE
        // ==========================================================


        public async Task<ResponseDTO> DeleteVehicleBookingReportAsync(Guid id)
        {
            var report = await _unitOfWork.VehicleBookingReportRepo.GetByIdAsync(id);
            if (report == null)
                    return new ResponseDTO("Không tìm thấy báo cáo để xoá.", 404, false);

                 _unitOfWork.VehicleBookingReportRepo.Delete(report);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("Xoá VehicleBookingReport thành công.", 200, true);
            }



        // ==========================================================
        // 🔹 (Tuỳ chọn) GET BY BOOKING ID
        // ==========================================================
        public async Task<ResponseDTO> GetReportsByBookingIdAsync(Guid bookingId)
        {
            try
            {
                var reports = await _unitOfWork.VehicleBookingReportRepo.GetAll()
                    .Where(r => r.VehicleBookingId == bookingId)
                    .Include(r => r.VehicleBooking)
                    .ToListAsync();

                return new ResponseDTO("Lấy danh sách báo cáo theo BookingId thành công.", 200, true, reports);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy báo cáo theo BookingId: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Xóa báo cáo thành công.", 200, true);
        }
    }
}