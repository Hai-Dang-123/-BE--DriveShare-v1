using BLL.Services.Interface;
using Common.DTOs;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ResponseDTO> GetAllItemContractsAsync()
        {
            try
            {
                var contracts = await _unitOfWork.ItemContractRepo
                    .GetAll()
                    .Include(c => c.ItemBooking)
                    .ToListAsync();

                if (!contracts.Any())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Không có hợp đồng hàng hoá nào."
                    };
                }

                var result = contracts.Select(c => new
                {
                    c.ContractId,
                    c.Version,
                    c.Status,
                    c.OwnerSigned,
                    c.RenterSigned,
                    c.CreatedAt,
                    c.SignedAt,
                    c.ItemBookingId
                }).ToList();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Lấy danh sách hợp đồng hàng hoá thành công.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = $"Lỗi khi lấy danh sách hợp đồng: {ex.Message}"
                };
            }
        }
        public async Task<ResponseDTO> UpdateItemContractAsync(Guid id, CreateItemContractDto dto)
        {
            try
            {
                var existing = await _unitOfWork.ItemContractRepo.GetByIdAsync(id);
                if (existing == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Không tìm thấy hợp đồng để cập nhật."
                    };
                }

                existing.Version = dto.version;
                existing.ContractTemplateId = dto.ContractTemplateId;
                existing.ItemBookingId = dto.ItemBookingId;
                existing.SignedAt = DateTime.UtcNow;
                existing.Status = Common.Enums.ContractStatus.ACTIVE;

                await _unitOfWork.ItemContractRepo.UpdateAsync(existing);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Cập nhật hợp đồng hàng hoá thành công.",
                    Result = existing
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = $"Lỗi khi cập nhật hợp đồng hàng hoá: {ex.Message}"
                };
            }
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
