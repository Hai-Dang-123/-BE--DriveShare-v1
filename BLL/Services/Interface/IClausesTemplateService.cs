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
        Task<ResponseDTO> CreateClauseTemplateAsync(CreateClauseTemplateDTO createClauseDTO);
        Task<ResponseDTO> UpdateClauseTemplateAsync(UpdateClauseTemplateDTO updateClauseDTO);
        Task<ResponseDTO> GetClauseTemplateByIdAsync(Guid clauseId);
        Task<ResponseDTO> DeleteClauseTemplateAsync(Guid clauseId);
        Task<ResponseDTO> GetAllClausesTemplateAsync();

    }
}
