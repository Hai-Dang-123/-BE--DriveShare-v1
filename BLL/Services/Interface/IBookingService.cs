using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBookingService
    {
        Task<ResponseDTO> CreateBookingAsync(CreateBookingDTO dto);
        Task<ResponseDTO> UpdateBookingAsync(Guid bookingId, CreateBookingDTO dto);
        Task<ResponseDTO> DeleteBookingAsync(Guid bookingId);
        Task<ResponseDTO> GetAllBookingsAsync();
        Task<ResponseDTO> GetBookingByIdAsync(Guid bookingId);
    }
}
