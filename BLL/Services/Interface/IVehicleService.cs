using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IVehicleService
    {
        Task<ResponseDTO> CreateVehicleAsync(CreateVehicleDTO dto);
        Task<ResponseDTO> GetAllVehiclesAsync();
        Task<ResponseDTO> GetVehicleByIdAsync(Guid id);
        Task<ResponseDTO> UpdateVehicleAsync(UpdateVehicleDTO dto);
        Task<ResponseDTO> DeleteVehicleAsync(Guid id);
        Task<ResponseDTO> ChangeStatusAsync(ChangeVehicleStatusDTO dto);

    }
}
