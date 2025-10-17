using Common.DTOs;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IReportService
    {
        Task<ResponseDTO> DeleteReportAsync(Guid id);
    }
}
