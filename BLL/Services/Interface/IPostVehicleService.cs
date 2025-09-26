using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IPostVehicleService 
    {
        Task<ResponseDTO> CreatePostVehicleAsync(CreateRequestPostVehicleDTO dto);
        Task<ResponseDTO> UpdatePostVehicleAsync(UpdateRequestPostVehicleDTO dto);
        Task<ResponseDTO> GetAllPostVehiclesOwner();
        Task<ResponseDTO> GetPostVehicleByIdAsync(Guid postId);
        //Task<ResponseDTO> ChangePostVehicleStatusAsync(ChangeStatusPostVehicleDTO dto);
        Task<ResponseDTO> DeletePostVehicleAsync(Guid postId);
        //Task<ResponseDTO> GetAllPostVehiclesByStatusAsync();
    }
}
