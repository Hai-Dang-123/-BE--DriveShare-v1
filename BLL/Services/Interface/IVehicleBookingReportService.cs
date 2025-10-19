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
        Task<ResponseDTO> GetAllVehicleBookingReportsAsync();
        Task<ResponseDTO> GetVehicleBookingReportByIdAsync(Guid id);
        Task<ResponseDTO> CreateVehicleBookingReportAsync(CreateVehicleBookingReportDTO dto);
        Task<ResponseDTO> UpdateVehicleBookingReportAsync(Guid id, CreateVehicleBookingReportDTO dto);
        Task<ResponseDTO> DeleteVehicleBookingReportAsync(Guid id);
    }
}
