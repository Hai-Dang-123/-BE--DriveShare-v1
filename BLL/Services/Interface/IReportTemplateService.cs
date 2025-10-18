using Common.DTOs;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IReportTemplateService
    {
        Task<ResponseDTO> DeleteReportTemplateAsync(Guid id);
        Task<ResponseDTO> GetAllReportTemplatesAsync();
        Task<ResponseDTO> GetReportTemplateByIdAsync(Guid id);
    }
}
