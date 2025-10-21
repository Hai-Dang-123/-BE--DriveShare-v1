using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IPostItemShippingRouteService
    {
        Task<ResponseDTO> CreatePostItemShippingRouteAsync(CreatePostItemShippingRouteRequest request);
        Task<ResponseDTO> UpdatePostItemShippingRouteAsync(CreatePostItemShippingRouteRequest request);
        Task<ResponseDTO> GetALLPostItemShippingRouteAsync();
        Task<ResponseDTO> GetByIdPostItemShippingRouteAsync(Guid id);
    }
}
