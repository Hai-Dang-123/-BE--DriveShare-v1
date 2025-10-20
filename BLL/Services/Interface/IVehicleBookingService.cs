using Common.DTOs;
using Common.Enums;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IVehicleBookingService
    {
        Task<ResponseDTO> CreateVehicleBookingAsync(CreateBookingDTO dto);
        Task<ResponseDTO> ChangeStatusAsync(Guid vehicleBookingId, BookingStatus newStatus);
        Task<ResponseDTO> GetBookingsByCurrentUserAsync();
        Task<ResponseDTO> GetBookingByIdAsync(Guid bookingId);
    }
}
