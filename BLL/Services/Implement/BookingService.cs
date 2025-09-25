using BLL.Services.Interface;
using Common.DTOs;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public  class BookingService : IBookingService
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
    }
}