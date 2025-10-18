using Common.DTOs;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IItemContractService
    {
        Task<ResponseDTO> DeleteItemContractAsync(Guid id);
    }
}
