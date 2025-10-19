using BLL.Services.Interface;
using Common.DTOs;
using Common.Enums;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class VehicleContractService : IVehicleContractService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleContractService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDTO> GetAllVehicleContractsAsync()
        {
            try
            {
                var contracts = await _unitOfWork.VehicleContractRepo.GetAll().ToListAsync();

                if (!contracts.Any())
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Không có hợp đồng xe nào."
                    };

                var result = contracts.Select(c => new ContractResponseDTO
                {
                    ContractId = c.ContractId,
                    Version = c.Version,
                    OwnerSigned = c.OwnerSigned,
                    RenterSigned = c.RenterSigned,
                    Status = c.Status.ToString(),
                    ContractTemplateId = c.ContractTemplateId,
                    VehicleBookingId = c.VehicleBookingId,
                    CreatedAt = c.CreatedAt,
                    SignedAt = c.SignedAt
                }).ToList();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Lấy danh sách hợp đồng xe thành công.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi lấy danh sách VehicleContracts: " + ex.Message
                };
            }
        }
        public async Task<ResponseDTO> UpdateVehicleContractAsync(Guid id, CreateVehicleContractDto dto)
        {
            try
            {
                var existing = await _unitOfWork.VehicleContractRepo.GetByIdAsync(id);

                if (existing == null)
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy hợp đồng xe cần cập nhật."
                    };

                existing.Version = dto.Version;
                existing.ContractTemplateId = dto.ContractTemplateId;
                existing.VehicleBookingId = dto.VehicleBookingId;
                existing.SignedAt = DateTime.UtcNow;
                existing.Status = ContractStatus.ACTIVE;

                _unitOfWork.VehicleContractRepo.UpdateAsync(existing);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Cập nhật hợp đồng xe thành công.",
                    Result = existing
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi cập nhật VehicleContract: " + ex.Message
                };
            }
        }
        public async Task<ResponseDTO> DeleteVehicleContractAsync(Guid id)
        {
            try
            {
                var contract = await _unitOfWork.VehicleContractRepo.GetByIdAsync(id);
                if (contract == null)
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy hợp đồng xe để xoá."
                    };

                _unitOfWork.VehicleContractRepo.Delete(contract);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Xoá hợp đồng xe thành công."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi xoá VehicleContract: " + ex.Message
                };
            }
        }
    }
}
