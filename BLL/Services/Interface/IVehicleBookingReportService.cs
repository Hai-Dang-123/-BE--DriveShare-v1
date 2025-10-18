using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IVehicleBookingReportService
    {
        Task<ResponseDTO> CreateReportAsync(CreateVehicleBookingReportDTO dto);
        Task<ResponseDTO> GetAllReportsAsync();
        Task<ResponseDTO> GetReportByIdAsync(Guid id);
        Task<ResponseDTO> UpdateReportAsync(Guid id, CreateVehicleBookingReportDTO dto);
        Task<ResponseDTO> DeleteReportAsync(Guid id);
    }
}
