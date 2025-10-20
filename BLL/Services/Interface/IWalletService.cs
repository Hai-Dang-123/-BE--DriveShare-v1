using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IWalletService
    {
        Task<ResponseDTO> CreateWalletAsync();
        Task<ResponseDTO> GetWalletByUserId();
        Task<ResponseDTO> DepositAsync(decimal amount);
        Task<ResponseDTO> WithdrawAsync(decimal amount);
    }
}
