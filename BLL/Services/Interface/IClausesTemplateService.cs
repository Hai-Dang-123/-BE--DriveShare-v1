using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IClausesTemplateService
    {
        Task<ResponseDTO> CreateClauseAsync(CreateClauseTemplateDTO createClauseDTO);
        
        Task<ResponseDTO> UpdateClauseAsync(UpdateClauseTemplateDTO updateClauseDTO);
        Task<ResponseDTO> GetClauseByIdAsync(Guid clauseId);
        Task<ResponseDTO> GetAllClausesAsync();

    }
}
