using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IITemServices
    {
        Task<ResponseDTO> CreateItemAsync(CreateItemDTO createItemDTO);
        Task<ResponseDTO> UpdateItemAsync(UpdateItemDTO updateItemDTO);
       // Task<ResponseDTO> GetItemByIdAsync(Guid itemId);
        //Task<ResponseDTO> DeleteItemAsync(Guid itemId);
       // Task<ResponseDTO> GetAllItemsAsync();
    }
}
