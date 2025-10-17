using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IClauseTermServices
    {
        Task<ResponseDTO> CreateClauseTermAync(ClauseTermDTO clauseTermDTO);
        Task<ResponseDTO> UpdateClauseTermAsync(UpdateClauseTermDTO clauseTermDTO);
        Task<ResponseDTO> DeleteClauseTermAsync(Guid ClauseTermId);
        Task<ResponseDTO> GetAllClauseTermsAsync();
        Task<ResponseDTO> GetClauseTermByIdAsync(Guid ClauseTermId);

    }
}
