using Common.DTOs;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IVehicleContractService
    {
        Task<ResponseDTO> GetAllVehicleContractsAsync();
        Task<ResponseDTO> UpdateVehicleContractAsync(Guid id, CreateVehicleContractDto dto);
        Task<ResponseDTO> DeleteVehicleContractAsync(Guid id);
    }
}
