using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IItemCharacteristicsService
    {
        Task<ResponseDTO> CreateItemCharacteristicsAsnc(CreateItemCharacteristicsDTO createItemCharacteristicsDTO);
        Task<ResponseDTO> UpdateItemCharacteristicsAsync(CreateItemCharacteristicsDTO updateItemCharacteristicsDTO);
    }
}
