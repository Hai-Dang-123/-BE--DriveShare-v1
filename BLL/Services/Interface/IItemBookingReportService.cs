using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IItemBookingReportService
    {
        Task<ResponseDTO> CreateReportAsync(CreateItemBookingReportDTO dto);
        Task<ResponseDTO> GetAllReportsAsync();
        Task<ResponseDTO> GetReportByIdAsync(Guid id);
        Task<ResponseDTO> UpdateReportAsync(Guid id, CreateItemBookingReportDTO dto);
        Task<ResponseDTO> DeleteReportAsync(Guid id);
    }
}
