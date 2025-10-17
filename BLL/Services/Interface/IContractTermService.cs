using Common.DTOs;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IContractTermService 
    {
        Task<ResponseDTO> CreateContractTermsAsync(ContracttermDTO contractTermsDTO);
        Task<ResponseDTO> UpdateContractTermsAsync(UpdateContracttermDTO contractTermsDTO);
        Task<ResponseDTO> DeleteContractTermsAsync(Guid ContractTermid);
        Task<ResponseDTO> GetAllContractTermsAsync();
        Task<ResponseDTO> GetContractTermsAsync(Guid ContractTermid);
    }
}
