using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTOs;
using Common.Enums;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class VehicleBookingService : IVehicleBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        private readonly ILogger<VehicleBookingService> _logger;

        public VehicleBookingService(IUnitOfWork unitOfWork, UserUtility userUtility, ILogger<VehicleBookingService> logger)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
            _logger = logger;
        }

        // ✅ CREATE BOOKING
        public async Task<ResponseDTO> CreateVehicleBookingAsync(CreateBookingDTO dto)
        {
            try
            {
                // Validate
                if (dto.PostVehicleId == Guid.Empty)
                    return new ResponseDTO("PostVehicleId không hợp lệ.", 400, false);

                if (dto.StartDate == null || dto.EndDate == null)
                    return new ResponseDTO("Ngày bắt đầu và kết thúc không được để trống.", 400, false);

                if (dto.EndDate <= dto.StartDate)
                    return new ResponseDTO("Ngày kết thúc phải sau ngày bắt đầu.", 400, false);

                // Lấy UserId từ Token (ưu tiên UserUtility)
                var renterId = _userUtility.GetUserIdFromToken();
                if (renterId == Guid.Empty)
                    renterId = dto.UserId; // fallback nếu test Swagger

                // Kiểm tra bài đăng xe có tồn tại không
                var postVehicle = await _unitOfWork.PostVehicleRepo.GetByIdAsync(dto.PostVehicleId);
                if (postVehicle == null)
                    return new ResponseDTO("Không tìm thấy bài đăng xe.", 404, false);

                // Kiểm tra thời gian thuê có nằm trong khoảng cho phép không
                if (dto.StartDate < postVehicle.AvailableStartDate || dto.EndDate > postVehicle.AvailableEndDate)
                    return new ResponseDTO("Thời gian thuê nằm ngoài phạm vi bài đăng xe.", 400, false);

                // Kiểm tra có trùng booking nào khác không
                var overlap = await _unitOfWork.VehicleBookingRepo.GetAll()
                    .AnyAsync(b =>
                        b.PostVehicleId == dto.PostVehicleId &&
                        b.Status != BookingStatus.CANCELED &&
                        b.Status != BookingStatus.REJECTED &&
                        b.RentalStartDate < dto.EndDate &&
                        dto.StartDate < b.RentalEndDate
                    );

                if (overlap)
                    return new ResponseDTO("Xe đã có booking trùng thời gian.", 409, false);

                // Tính số ngày thuê
                var days = (dto.EndDate.Value.Date - dto.StartDate.Value.Date).TotalDays;
                if (days < 1) days = 1;

                // Tính tổng tiền (giá thuê mỗi ngày * số ngày)
                var totalPrice = postVehicle.DailyPrice * (decimal)days;

                // Tạo VehicleBooking mới
                var booking = new VehicleBooking
                {
                    VehicleBookingId = Guid.NewGuid(),
                    PostVehicleId = dto.PostVehicleId,
                    RenterUserId = renterId,
                    RentalStartDate = dto.StartDate.Value,
                    RentalEndDate = dto.EndDate.Value,
                    TotalPrice = totalPrice,
                    Status = BookingStatus.PENDING,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.VehicleBookingRepo.AddAsync(booking);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("Tạo booking thành công.", 201, true, new
                {
                    booking.VehicleBookingId,
                    booking.TotalPrice,
                    booking.Status
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo VehicleBooking.");
                return new ResponseDTO($"Lỗi khi tạo booking: {ex.Message}", 500, false);
            }
        }

        // ✅ CHANGE STATUS
        public async Task<ResponseDTO> ChangeStatusAsync(Guid vehicleBookingId, BookingStatus newStatus)
        {
            try
            {
                var booking = await _unitOfWork.VehicleBookingRepo.GetByIdAsync(vehicleBookingId);
                if (booking == null)
                    return new ResponseDTO("Không tìm thấy booking.", 404, false);

                // Ngăn thay đổi nếu booking đã kết thúc
                if (booking.Status == BookingStatus.COMPLETED || booking.Status == BookingStatus.CANCELED)
                    return new ResponseDTO("Booking đã hoàn tất hoặc bị hủy, không thể thay đổi trạng thái.", 400, false);

                // Gán trạng thái mới
                booking.Status = newStatus;
                await _unitOfWork.VehicleBookingRepo.UpdateAsync(booking);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("Cập nhật trạng thái thành công.", 200, true, new
                {
                    booking.VehicleBookingId,
                    booking.Status
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi thay đổi trạng thái VehicleBooking.");
                return new ResponseDTO($"Lỗi khi thay đổi trạng thái: {ex.Message}", 500, false);
            }
        }
        public async Task<ResponseDTO> GetBookingsByCurrentUserAsync()
        {
            try
            {
                var renterUserId = _userUtility.GetUserIdFromToken();

                if (renterUserId == Guid.Empty)
                    return new ResponseDTO("Không xác định được người dùng từ token.", 401, false);

                var bookings = await _unitOfWork.VehicleBookingRepo
                    .GetAll()
                    .Include(vb => vb.PostVehicle)
                        .ThenInclude(pv => pv.Vehicle)
                    .Where(vb => vb.RenterUserId == renterUserId)
                    .OrderByDescending(vb => vb.CreatedAt)
                    .Select(vb => new
                    {
                        vb.VehicleBookingId,
                        vb.PostVehicleId,
                        VehicleName = vb.PostVehicle.Vehicle.Brand + " " + vb.PostVehicle.Vehicle.Model,
                        vb.TotalPrice,
                        vb.RentalStartDate,
                        vb.RentalEndDate,
                        vb.Status,
                        vb.CreatedAt
                    })
                    .ToListAsync();

                if (!bookings.Any())
                    return new ResponseDTO("Không có booking nào của người dùng này.", 404, false);

                return new ResponseDTO("Lấy danh sách booking thành công.", 200, true, bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách booking của user hiện tại.");
                return new ResponseDTO($"Lỗi hệ thống: {ex.Message}", 500, false);
            }
        }

        // ✅ Lấy chi tiết 1 booking theo VehicleBookingId
        public async Task<ResponseDTO> GetBookingByIdAsync(Guid bookingId)
        {
            try
            {
                var renterUserId = _userUtility.GetUserIdFromToken();

                if (renterUserId == Guid.Empty)
                    return new ResponseDTO("Không xác định được người dùng từ token.", 401, false);

                var booking = await _unitOfWork.VehicleBookingRepo
                    .GetAll()
                    .Include(vb => vb.PostVehicle)
                        .ThenInclude(pv => pv.Vehicle)
                    .Include(vb => vb.RenterUser)
                    .FirstOrDefaultAsync(vb => vb.VehicleBookingId == bookingId && vb.RenterUserId == renterUserId);

                if (booking == null)
                    return new ResponseDTO("Không tìm thấy booking hoặc bạn không có quyền truy cập.", 404, false);

                var result = new
                {
                    booking.VehicleBookingId,
                    booking.TotalPrice,
                    booking.RentalStartDate,
                    booking.RentalEndDate,
                    booking.Status,
                    booking.CreatedAt,
                    VehicleInfo = new
                    {
                        booking.PostVehicle.PostVehicleId,
                        VehicleName = booking.PostVehicle.Vehicle.Brand + " " + booking.PostVehicle.Vehicle.Model,

                        booking.PostVehicle.DailyPrice
                    },
                    RenterInfo = new
                    {
                        booking.RenterUser.UserId,
                        booking.RenterUser.Username,
                        booking.RenterUser.Email
                    }
                };

                return new ResponseDTO("Lấy chi tiết booking thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy chi tiết booking.");
                return new ResponseDTO($"Lỗi hệ thống: {ex.Message}", 500, false);
            }
        }
    }
}
