using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IAddOptionService
    {
        Task<ResponseDTO> CreateAddOptionAsync(CreateAddOptionDTO dto);
        Task<ResponseDTO> GetAllAddOptionsAsync();
        Task<ResponseDTO> GetAddOptionByIdAsync(Guid id);
        Task<ResponseDTO> UpdateAddOptionAsync(Guid id, CreateAddOptionDTO dto);
        Task<ResponseDTO> DeleteAddOptionAsync(Guid id);
    }
}
