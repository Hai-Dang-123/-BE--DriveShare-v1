using BLL.Services.Interface;
using Common.DTOs;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ContractService : IContractService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContractService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreateItemContractAsync(CreateItemContractDto createItemContractDto)
        {
            try
            {
                var teplate = _unitOfWork.ContractTemplateRepo.GetByIdWithTermsAsync(createItemContractDto.ContractTemplateId);
                if (teplate == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Contract template not found."
                    };
                }
                var newContract = new ItemContract
                {
                    ContractId = Guid.NewGuid(),
                    Version = createItemContractDto.version,
                    OwnerSigned = true,
                    RenterSigned = false,
                    CreatedAt = DateTime.UtcNow,
                    SignedAt = DateTime.Now,
                    ContractTemplateId = createItemContractDto.ContractTemplateId,
                    Status = Common.Enums.ContractStatus.DRAFT,
                    ItemBookingId = createItemContractDto.ItemBookingId
                };
                await _unitOfWork.ItemContractRepo.AddAsync(newContract);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Item contract created successfully.",
                    Result = newContract
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while creating the item contract: {ex.Message}"
                };

            }
        }

        public async Task<ResponseDTO> CreatVehicleContractAsync(CreateVehicleContractDto createVehicleContractDto)
        {
            try
            {
                var template = await _unitOfWork.ContractTemplateRepo.GetByIdWithTermsAsync(createVehicleContractDto.ContractTemplateId);
                if (template == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Contract template not found."
                    };
                }
                var newContract = new VehicleContract
                {
                    ContractId = Guid.NewGuid(),
                    Version = createVehicleContractDto.Version,
                    OwnerSigned = true,
                    RenterSigned = false,
                    CreatedAt = DateTime.UtcNow,
                    SignedAt = DateTime.Now,
                    ContractTemplateId = template.ContractTemplateId,
                    Status = Common.Enums.ContractStatus.DRAFT,
                    VehicleBookingId = createVehicleContractDto.VehicleBookingId
                };
                await _unitOfWork.vehicleContractRepo.AddAsync(newContract);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Vehicle contract created successfully.",
                    Result = newContract
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while creating the vehicle contract: {ex.Message}"
                };
            }
        }
    }
}
