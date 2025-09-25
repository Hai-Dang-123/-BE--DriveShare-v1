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
    }
}
