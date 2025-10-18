using Common.DTOs;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IClauseTermService
    {
        Task<ResponseDTO> DeleteClauseTermAsync(Guid id);
    }
}
