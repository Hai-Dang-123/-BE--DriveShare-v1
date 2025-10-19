using Common.DTOs;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IItemContractService
    {
        Task<ResponseDTO> GetAllItemContractsAsync();
        Task<ResponseDTO> UpdateItemContractAsync(Guid id, CreateItemContractDto dto);
        Task<ResponseDTO> DeleteItemContractAsync(Guid id);
    }
}
