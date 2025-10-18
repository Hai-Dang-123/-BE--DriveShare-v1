using BLL.Services.Interface;
using Common.DTOs;
using DAL.UnitOfWork;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ItemContractService : IItemContractService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ItemContractService> _logger;

        public ItemContractService(IUnitOfWork unitOfWork, ILogger<ItemContractService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ResponseDTO> DeleteItemContractAsync(Guid id)
        {
            try
            {
                var itemContract = await _unitOfWork.ItemContractRepo.GetByIdAsync(id);
                if (itemContract == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Item contract not found."
                    };
                }

                _unitOfWork.ItemContractRepo.Delete(itemContract);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Item contract deleted successfully."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting item contract");
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Error deleting item contract: {ex.Message}"
                };
            }
        }
    }
}
