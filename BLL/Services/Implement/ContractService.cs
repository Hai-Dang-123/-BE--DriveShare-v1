using BLL.Services.Interface;
using Common.DTOs;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        // GetAllItemContractsAsync
        // GetAllVehicleContractsAsync
        // UpdateItemContractAsync
        // UpdateVehicleContractAsync
        // DeleteContractAsync

        public async Task<ResponseDTO> GetAllContractsAsync()
        {
            try
            {
                var contracts = _unitOfWork.ContractRepo.GetAll().ToList();



                if (!contracts.Any())
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Không có hợp đồng nào."
                    };
                }

                var result = contracts.Select(c =>
                {
                    if (c is VehicleContract vc)
                    {
                        return new ContractResponseDTO
                        {
                            ContractId = vc.ContractId,
                            Version = vc.Version,
                            OwnerSigned = vc.OwnerSigned,
                            RenterSigned = vc.RenterSigned,
                            Status = vc.Status.ToString(),
                            ContractTemplateId = vc.ContractTemplateId,
                            VehicleBookingId = vc.VehicleBookingId,
                            CreatedAt = vc.CreatedAt,
                            SignedAt = vc.SignedAt
                        };
                    }
                    else if (c is ItemContract ic)
                    {
                        return new ContractResponseDTO
                        {
                            ContractId = ic.ContractId,
                            Version = ic.Version,
                            OwnerSigned = ic.OwnerSigned,
                            RenterSigned = ic.RenterSigned,
                            Status = ic.Status.ToString(),
                            ContractTemplateId = ic.ContractTemplateId,
                            ItemBookingId = ic.ItemBookingId,
                            CreatedAt = ic.CreatedAt,
                            SignedAt = ic.SignedAt
                        };
                    }
                    else
                    {
                        return new ContractResponseDTO
                        {
                            ContractId = c.ContractId,
                            Version = c.Version,
                            OwnerSigned = c.OwnerSigned,
                            RenterSigned = c.RenterSigned,
                            Status = c.Status.ToString(),
                            ContractTemplateId = c.ContractTemplateId,
                            CreatedAt = c.CreatedAt,
                            SignedAt = c.SignedAt
                        };
                    }
                }).ToList();


                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Lấy danh sách hợp đồng thành công.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = "Lỗi khi lấy danh sách hợp đồng: " + ex.Message
                };
            }
        }
        public async Task<ResponseDTO> GetContractByIdAsync(Guid id)
        {
            try
            {
                var c = await _unitOfWork.ContractRepo.GetByIdAsync(id);

                if (c == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Không tìm thấy hợp đồng."
                    };
                }
                ContractResponseDTO result;

                if (c is VehicleContract vc)
                {
                    result = new ContractResponseDTO
                    {
                        ContractId = vc.ContractId,
                        Version = vc.Version,
                        OwnerSigned = vc.OwnerSigned,
                        RenterSigned = vc.RenterSigned,
                        Status = vc.Status.ToString(),
                        ContractTemplateId = vc.ContractTemplateId,
                        VehicleBookingId = vc.VehicleBookingId,
                        CreatedAt = vc.CreatedAt,
                        SignedAt = vc.SignedAt
                    };
                }
                else if (c is ItemContract ic)
                {
                    result = new ContractResponseDTO
                    {
                        ContractId = ic.ContractId,
                        Version = ic.Version,
                        OwnerSigned = ic.OwnerSigned,
                        RenterSigned = ic.RenterSigned,
                        Status = ic.Status.ToString(),
                        ContractTemplateId = ic.ContractTemplateId,
                        ItemBookingId = ic.ItemBookingId,
                        CreatedAt = ic.CreatedAt,
                        SignedAt = ic.SignedAt
                    };
                }
                else
                {
                    result = new ContractResponseDTO
                    {
                        ContractId = c.ContractId,
                        Version = c.Version,
                        OwnerSigned = c.OwnerSigned,
                        RenterSigned = c.RenterSigned,
                        Status = c.Status.ToString(),
                        ContractTemplateId = c.ContractTemplateId,
                        CreatedAt = c.CreatedAt,
                        SignedAt = c.SignedAt
                    };
                }


                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Lấy chi tiết hợp đồng thành công.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = "Lỗi khi lấy hợp đồng: " + ex.Message
                };
            }
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
