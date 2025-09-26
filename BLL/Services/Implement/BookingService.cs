using BLL.Services.Interface;
using Common.DTOs;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ResponseDTO> CreateBookingAsync(CreateBookingDTO dto)
        {

            if (dto == null)
                return new ResponseDTO("Dữ liệu không hợp lệ", 400, false);


            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                PostVehicleId = dto.PostVehicleId,
                UserId = dto.UserId,
                TotalPrice = dto.TotalPrice,
                Confirmed = false,
                Status = Common.Enums.BookingStatus.PENDING,
                CreatedAt = DateTime.UtcNow,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            try
            {
                await _unitOfWork.BookingRepo.AddAsync(booking);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO("Có lỗi xảy ra khi tạo booking", 500, false);
            }

            return new ResponseDTO("Tạo booking thành công", 201, true, booking.BookingId);
        }
        public async Task<ResponseDTO> UpdateBookingAsync(Guid bookingId, CreateBookingDTO dto)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
                return new ResponseDTO("Booking không tồn tại", 404, false);

            booking.PostVehicleId = dto.PostVehicleId;
            booking.UserId = dto.UserId;
            booking.TotalPrice = dto.TotalPrice;
            booking.StartDate = dto.StartDate;
            booking.EndDate = dto.EndDate;

            try
            {
                await _unitOfWork.BookingRepo.UpdateAsync(booking);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO("Cập nhật booking thành công", 200, true, booking.BookingId);
            }
            catch
            {
                return new ResponseDTO("Có lỗi xảy ra khi cập nhật booking", 500, false);
            }
        }

        public async Task<ResponseDTO> DeleteBookingAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
                return new ResponseDTO("Booking không tồn tại", 404, false);

            try
            {
                await _unitOfWork.BookingRepo.DeleteAsync(bookingId);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO("Xóa booking thành công", 200, true, bookingId);
            }
            catch
            {
                return new ResponseDTO("Có lỗi xảy ra khi xóa booking", 500, false);
            }
        }
        public async Task<ResponseDTO> GetAllBookingsAsync()
        {
            var bookings = _unitOfWork.BookingRepo.GetAll();
            var bookingList = bookings?
                .Select(b => new ListBookingDTO
                {
                    BookingId = b.BookingId,
                    PostVehicleId = b.PostVehicleId,
                    UserId = b.UserId,
                    TotalPrice = b.TotalPrice,
                    Confirmed = b.Confirmed,
                    Status = (int)b.Status,
                    CreatedAt = b.CreatedAt,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate
                }).ToList() ?? new List<ListBookingDTO>();

            return new ResponseDTO("Lấy danh sách booking thành công", 200, true, bookingList);
        }

        public async Task<ResponseDTO> GetBookingByIdAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
                return new ResponseDTO("Booking không tồn tại", 200, true, null);

            var bookingDto = new ListBookingDTO
            {
                BookingId = booking.BookingId,
                PostVehicleId = booking.PostVehicleId,
                UserId = booking.UserId,
                TotalPrice = booking.TotalPrice,
                Confirmed = booking.Confirmed,
                Status = (int)booking.Status,
                CreatedAt = booking.CreatedAt,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate
            };

            return new ResponseDTO("Lấy booking thành công", 200, true, bookingDto);
        }
    }
}