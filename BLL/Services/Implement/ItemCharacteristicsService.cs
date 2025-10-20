using BLL.Services.Interface;
using Common.DTOs;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ItemCharacteristicsService : IItemCharacteristicsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ItemCharacteristicsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreateItemCharacteristicsAsnc(CreateItemCharacteristicsDTO createItemCharacteristicsDTO)
        {
            try
            {
                var characteristics = new ItemCharacteristics
                {
                    ItemId = createItemCharacteristicsDTO.ItemId,
                    IsFragile = createItemCharacteristicsDTO.IsFragile,
                    IsFlammable = createItemCharacteristicsDTO.IsFlammable,
                    IsPerishable = createItemCharacteristicsDTO.IsPerishable,
                    RequiresRefrigeration = createItemCharacteristicsDTO.RequiresRefrigeration,
                    IsOversized = createItemCharacteristicsDTO.IsOversized,
                    IsHazardous = createItemCharacteristicsDTO.IsHazardous,
                    IsProhibited = createItemCharacteristicsDTO.IsProhibited,
                    RequiresInsurance = createItemCharacteristicsDTO.RequiresInsurance,
                    RequiresSpecialHandling = createItemCharacteristicsDTO.RequiresSpecialHandling,
                    OtherRequirements = createItemCharacteristicsDTO.OtherRequirements
                };

                await _unitOfWork.ItemCharacteristicsRepo.AddAsync(characteristics);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Item characteristics created successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> GetAllItemCharacteristicsAsync()
        {
            try
            {
                var ItemCharacteristics = await _unitOfWork.ItemCharacteristicsRepo.GetAllItemCharacteristicsAsync();
                if (ItemCharacteristics == null || !ItemCharacteristics.Any())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No item characteristics found"
                    };
                }
                var ItemCharacteristicsDTOs = ItemCharacteristics.Select(c => new CreateItemCharacteristicsDTO
                {
                    ItemId = c.ItemId,
                    IsFragile = c.IsFragile,
                    IsFlammable = c.IsFlammable,
                    IsPerishable = c.IsPerishable,
                    RequiresRefrigeration = c.RequiresRefrigeration,
                    IsOversized = c.IsOversized,
                    IsHazardous = c.IsHazardous,
                    IsProhibited = c.IsProhibited,
                    RequiresInsurance = c.RequiresInsurance,
                    RequiresSpecialHandling = c.RequiresSpecialHandling,
                    OtherRequirements = c.OtherRequirements
                }).ToList();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Item characteristics retrieved successfully",
                    Result = ItemCharacteristicsDTOs
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }

            }

        public async Task<ResponseDTO> GetItemCharacteristicsByIdAsync(Guid itemCharacteristicsId)
        {
            try
            {
                var characteristics = await _unitOfWork.ItemCharacteristicsRepo.GetByIdAsync(itemCharacteristicsId);
                if (characteristics == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Item characteristics not found"
                    };
                }
                var characteristicsDTO = new CreateItemCharacteristicsDTO
                {
                    ItemId = characteristics.ItemId,
                    IsFragile = characteristics.IsFragile,
                    IsFlammable = characteristics.IsFlammable,
                    IsPerishable = characteristics.IsPerishable,
                    RequiresRefrigeration = characteristics.RequiresRefrigeration,
                    IsOversized = characteristics.IsOversized,
                    IsHazardous = characteristics.IsHazardous,
                    IsProhibited = characteristics.IsProhibited,
                    RequiresInsurance = characteristics.RequiresInsurance,
                    RequiresSpecialHandling = characteristics.RequiresSpecialHandling,
                    OtherRequirements = characteristics.OtherRequirements
                };
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Item characteristics retrieved successfully",
                    Result = characteristicsDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> UpdateItemCharacteristicsAsync(CreateItemCharacteristicsDTO updateItemCharacteristicsDTO)
        {
            try
            {
                var ItemCharacteristics = await _unitOfWork.ItemCharacteristicsRepo.GetByIdAsync(updateItemCharacteristicsDTO.ItemId);
            if (ItemCharacteristics == null)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Item characteristics not found"
                };
            }
           
                ItemCharacteristics.IsFragile = updateItemCharacteristicsDTO.IsFragile;
                ItemCharacteristics.IsFlammable = updateItemCharacteristicsDTO.IsFlammable;
                ItemCharacteristics.IsPerishable = updateItemCharacteristicsDTO.IsPerishable;
                ItemCharacteristics.RequiresRefrigeration = updateItemCharacteristicsDTO.RequiresRefrigeration;
                ItemCharacteristics.IsOversized = updateItemCharacteristicsDTO.IsOversized;
                ItemCharacteristics.IsHazardous = updateItemCharacteristicsDTO.IsHazardous;
                ItemCharacteristics.IsProhibited = updateItemCharacteristicsDTO.IsProhibited;
                ItemCharacteristics.RequiresInsurance = updateItemCharacteristicsDTO.RequiresInsurance;
                ItemCharacteristics.RequiresSpecialHandling = updateItemCharacteristicsDTO.RequiresSpecialHandling;
                ItemCharacteristics.OtherRequirements = updateItemCharacteristicsDTO.OtherRequirements;
                await _unitOfWork.ItemCharacteristicsRepo.UpdateAsync(ItemCharacteristics);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Item characteristics updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }
    }
}
