using Common.DTOs;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IContractTermService
    {
        Task<ResponseDTO> GetContractTermByIdAsync(Guid id);
        Task<ResponseDTO> GetAllContractTermsAsync();
    }
}
