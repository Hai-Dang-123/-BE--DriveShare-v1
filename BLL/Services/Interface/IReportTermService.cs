using Common.DTOs;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IReportTermService
    {
        Task<ResponseDTO> CreateReportTermAsync(CreateReportTermDTO dto);
        Task<ResponseDTO> UpdateReportTermAsync(Guid id, CreateReportTermDTO dto);
        Task<ResponseDTO> GetAllReportTermsAsync();
        Task<ResponseDTO> GetReportTermByIdAsync(Guid id);
        Task<ResponseDTO> DeleteReportTermAsync(Guid id);
    }
}
