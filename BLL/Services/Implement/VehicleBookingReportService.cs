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
    public class VehicleBookingReportService : IVehicleBookingReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleBookingReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ✅ GET ALL
        public async Task<ResponseDTO> GetAllVehicleBookingReportsAsync()
        {
            try
            {
                var reports = await _unitOfWork.VehicleBookingReportRepo
                    .GetAll()
                    .Include(r => r.VehicleBooking)
                        .ThenInclude(b => b.PostVehicle)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

                var result = reports.Select(r => new VehicleBookingReportDTO
                {
                    ReportId = r.ReportId,
                    VehicleBookingId = r.VehicleBookingId,
                    ReportTitle = r.ReportTitle,
                    Version = r.Version,
                    ReportType = r.ReportType,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt,
                    SignedAt = r.SignedAt,
                    ReportTemplateId = r.ReportTemplateId
                }).ToList();

                return new ResponseDTO("Lấy danh sách VehicleBookingReport thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy danh sách báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ GET BY ID
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

                var dto = new VehicleBookingReportDTO
                {
                    ReportId = report.ReportId,
                    VehicleBookingId = report.VehicleBookingId,
                    ReportTitle = report.ReportTitle,
                    Version = report.Version,
                    ReportType = report.ReportType,
                    Status = report.Status,
                    CreatedAt = report.CreatedAt,
                    SignedAt = report.SignedAt,
                    ReportTemplateId = report.ReportTemplateId
                };

                return new ResponseDTO("Lấy báo cáo thành công.", 200, true, dto);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ CREATE
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
                    ReportType = dto.ReportType,
                    Version = dto.Version,
                    Status = ReportStatus.PENDING,
                    // ⚙️ Fix: DTO là Guid? → dùng ?? Guid.Empty
                    ReportTemplateId = dto.ReportTemplateId ?? Guid.Empty,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.VehicleBookingReportRepo.AddAsync(entity);
                await _unitOfWork.SaveChangeAsync();

                var result = new VehicleBookingReportDTO
                {
                    ReportId = entity.ReportId,
                    VehicleBookingId = entity.VehicleBookingId,
                    ReportTitle = entity.ReportTitle,
                    Version = entity.Version,
                    ReportType = entity.ReportType,
                    Status = entity.Status,
                    CreatedAt = entity.CreatedAt,
                    ReportTemplateId = entity.ReportTemplateId
                };

                return new ResponseDTO("Tạo VehicleBookingReport thành công.", 201, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Đã xảy ra lỗi khi tạo báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ UPDATE
        public async Task<ResponseDTO> UpdateVehicleBookingReportAsync(Guid id, CreateVehicleBookingReportDTO dto)
        {
            var report = await _unitOfWork.VehicleBookingReportRepo.GetByIdAsync(id);
            if (report == null)
                return new ResponseDTO("Không tìm thấy báo cáo để cập nhật.", 404, false);

            report.ReportTitle = dto.ReportTitle;
            report.ReportType = dto.ReportType;
            report.Version = dto.Version;
            // ⚙️ Fix: Guid? -> Guid
            report.ReportTemplateId = dto.ReportTemplateId ?? Guid.Empty;
            report.VehicleBookingId = dto.VehicleBookingId;

            try
            {
                await _unitOfWork.VehicleBookingReportRepo.UpdateAsync(report);
                await _unitOfWork.SaveChangeAsync();

                var result = new VehicleBookingReportDTO
                {
                    ReportId = report.ReportId,
                    VehicleBookingId = report.VehicleBookingId,
                    ReportTitle = report.ReportTitle,
                    Version = report.Version,
                    ReportType = report.ReportType,
                    Status = report.Status,
                    CreatedAt = report.CreatedAt,
                    SignedAt = report.SignedAt,
                    ReportTemplateId = report.ReportTemplateId
                };

                return new ResponseDTO("Cập nhật VehicleBookingReport thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi cập nhật báo cáo: {ex.Message}", 500, false);
            }
        }

        // ✅ DELETE
        public async Task<ResponseDTO> DeleteVehicleBookingReportAsync(Guid id)
        {
            var report = await _unitOfWork.VehicleBookingReportRepo.GetByIdAsync(id);
            if (report == null)
                return new ResponseDTO("Không tìm thấy báo cáo để xoá.", 404, false);

            _unitOfWork.VehicleBookingReportRepo.Delete(report);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Xoá VehicleBookingReport thành công.", 200, true);
        }
    }
}
