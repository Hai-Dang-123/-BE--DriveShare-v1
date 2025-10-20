using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IPostItemService
    {
        Task<ResponseDTO> CreatePostItemAsync(CreatePostItemDTO createPostItemDTO);
        Task<ResponseDTO> UpdatePostItemAsync(UpdatePostItemDTO updatePostItemDTO);
        Task<ResponseDTO> GetPostItemByIdAsync(Guid postItemId);
        Task<ResponseDTO> GetAllPostItemsAsync();
        Task<ResponseDTO> DeletePostItemAsync(Guid postItemId);
    }
}
