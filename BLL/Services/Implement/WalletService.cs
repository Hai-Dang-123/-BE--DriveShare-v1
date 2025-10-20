using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTOs;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        public WalletService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }
        public async Task<ResponseDTO> CreateWalletAsync()
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO("Invalid user.", 400, false);
            }

            var existingWallet = await _unitOfWork.WalletRepo.GetByUserIdAsync(userId);
            if (existingWallet != null)
            {
                return new ResponseDTO("Wallet already exists.", 400, false);
            }
            var wallet = new DAL.Entities.Wallet
            {
                WalletId = Guid.NewGuid(),
                UserId = userId,
                Currency = "VND",
                CurrentBalance = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            };

            try
            {
                await _unitOfWork.WalletRepo.AddAsync(wallet);
                await _unitOfWork.SaveAsync();
                return new ResponseDTO("Wallet created successfully.", 201, true, wallet);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error creating wallet: {ex.Message}", 500, false);

            }
        }

        public async Task<ResponseDTO> DepositAsync(decimal amount)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO("Invalid user.", 400, false);
            }
            var wallet = await _unitOfWork.WalletRepo.GetByUserIdAsync(userId);
            if (wallet == null)
            {
                return new ResponseDTO("Wallet not found.", 404, false);
            }
            wallet.CurrentBalance += amount;
            wallet.UpdatedAt = DateTime.UtcNow;
            try
            {
                await _unitOfWork.WalletRepo.UpdateAsync(wallet);
                await _unitOfWork.SaveAsync();
                return new ResponseDTO("Deposit successful.", 200, true, wallet);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error during deposit: {ex.Message}", 500, false);
            }
        }

        public async Task<ResponseDTO> GetWalletByUserId()
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO("Invalid user.", 400, false);
            }
            var wallet = await _unitOfWork.WalletRepo.GetByUserIdAsync(userId);
            if (wallet == null)
            {
                return new ResponseDTO("Wallet not found.", 404, false);
            }

            var walletDTO = new WalletDTO
            {
                WalletId = wallet.WalletId,
                UserId = wallet.UserId,
                Currency = wallet.Currency,
                CurrentBalance = wallet.CurrentBalance,
                CreatedAt = wallet.CreatedAt,
                UpdatedAt = wallet.UpdatedAt
            };
            return new ResponseDTO("Wallet retrieved successfully.", 200, true, walletDTO);
        }

        public async Task<ResponseDTO> WithdrawAsync(decimal amount)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO("Invalid user.", 400, false);
            }
            var wallet = await _unitOfWork.WalletRepo.GetByUserIdAsync(userId);
            if (wallet == null)
            {
                return new ResponseDTO("Wallet not found.", 404, false);
            }
            if (wallet.CurrentBalance < amount)
            {
                return new ResponseDTO("Insufficient balance.", 400, false);
            }
            wallet.CurrentBalance -= amount;
            wallet.UpdatedAt = DateTime.UtcNow;
            try {                
                await _unitOfWork.WalletRepo.UpdateAsync(wallet);
                await _unitOfWork.SaveAsync();
                return new ResponseDTO("Withdrawal successful.", 200, true, wallet);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error during withdrawal: {ex.Message}", 500, false);
            }
        }
    }
}
