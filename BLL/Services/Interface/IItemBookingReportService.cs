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
        Task<ResponseDTO> GetAllItemReportsAsync();
        Task<ResponseDTO> GetItemReportByIdAsync(Guid id);
        Task<ResponseDTO> CreateItemReportAsync(CreateItemBookingReportDTO dto);
        Task<ResponseDTO> UpdateItemReportAsync(Guid id, CreateItemBookingReportDTO dto);
        Task<ResponseDTO> DeleteItemReportAsync(Guid id);

    }
}
