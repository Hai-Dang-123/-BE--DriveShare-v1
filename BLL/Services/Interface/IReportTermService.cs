using Common.DTOs;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IReportTermService
    {
        Task<ResponseDTO> DeleteReportTermAsync(Guid id);
        Task<ResponseDTO> GetAllReportTermsAsync();
        Task<ResponseDTO> GetReportTermByIdAsync(Guid id);
    }
}
