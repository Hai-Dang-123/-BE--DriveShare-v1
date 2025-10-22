using BLL.Services.Interface;
using Common.DTOs;
using Common.Enums;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ItemContractService : IItemContractService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemContractService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> GetAllItemContractsAsync()
        {
            try
            {
                var contracts = await _unitOfWork.ItemContractRepo
                    .GetAll()
                    .Include(c => c.ItemBooking)
                    .ToListAsync();

                var result = contracts.Select(c => new ItemContractDTO
                {
                    ContractId = c.ContractId,
                    Version = c.Version,
                    Status = c.Status,
                    OwnerSigned = c.OwnerSigned,
                    RenterSigned = c.RenterSigned,
                    CreatedAt = c.CreatedAt,
                    SignedAt = c.SignedAt,
                    ItemBookingId = c.ItemBookingId
                }).ToList();

                return new ResponseDTO("Lấy danh sách hợp đồng hàng hoá thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi lấy danh sách hợp đồng: {ex.Message}", 500, false);
            }
        }

        public async Task<ResponseDTO> UpdateItemContractAsync(Guid id, CreateItemContractDTO dto)
        {
            try
            {
                var existing = await _unitOfWork.ItemContractRepo.GetByIdAsync(id);
                if (existing == null)
                    return new ResponseDTO("Không tìm thấy hợp đồng để cập nhật.", 404, false);

                existing.Version = dto.Version;
                existing.ContractTemplateId = dto.ContractTemplateId;
                existing.ItemBookingId = dto.ItemBookingId;
                existing.SignedAt = DateTime.UtcNow;
                existing.Status = ContractStatus.ACTIVE;

                await _unitOfWork.ItemContractRepo.UpdateAsync(existing);
                await _unitOfWork.SaveChangeAsync();

                var result = new ItemContractDTO
                {
                    ContractId = existing.ContractId,
                    Version = existing.Version,
                    Status = existing.Status,
                    OwnerSigned = existing.OwnerSigned,
                    RenterSigned = existing.RenterSigned,
                    CreatedAt = existing.CreatedAt,
                    SignedAt = existing.SignedAt,
                    ItemBookingId = existing.ItemBookingId
                };

                return new ResponseDTO("Cập nhật hợp đồng hàng hoá thành công.", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi cập nhật hợp đồng hàng hoá: {ex.Message}", 500, false);
            }
        }

        public async Task<ResponseDTO> DeleteItemContractAsync(Guid id)
        {
            var itemContract = await _unitOfWork.ItemContractRepo.GetByIdAsync(id);
            if (itemContract == null)
                return new ResponseDTO("Không tìm thấy hợp đồng để xoá.", 404, false);

            _unitOfWork.ItemContractRepo.Delete(itemContract);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Xoá hợp đồng hàng hoá thành công.", 200, true);
        }
    }
}
