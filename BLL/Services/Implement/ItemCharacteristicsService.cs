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
