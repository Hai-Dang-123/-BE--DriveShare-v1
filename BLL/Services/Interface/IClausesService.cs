using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IClausesService
    {
        Task<ResponseDTO> CreateClauseAsync(CreateClauseDTO createClauseDTO);
        Task<ResponseDTO> GetAllClauseAsync();
        Task<ResponseDTO> UpdateClauseAsync(UpdateClauseDTO updateClauseDTO);
        Task<ResponseDTO> GetClauseByIdAsync(Guid id);
        Task<ResponseDTO> DeleteClauseAsync( Guid id);

    }
}
