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
    public class ITemServices : IITemServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ITemServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreateItemAsync(CreateItemDTO createItemDTO)
        {
            try
            {
                var newItem = new Item
                {
                    ItemName = createItemDTO.ItemName,
                    Description = createItemDTO.Description,
                    Unit = createItemDTO.Unit,
                    VolumeM3 = createItemDTO.VolumeM3,
                    WeightKg = createItemDTO.WeightKg,
                    Quantity = createItemDTO.Quantity,
                };
                await _unitOfWork.ItemRepo.AddAsync(newItem);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = "create new Item succes"
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
        public async Task<ResponseDTO> UpdateItemAsync(UpdateItemDTO updateItemDTO)
        {
            try
            {
                var Item = await _unitOfWork.ItemRepo.GetByIdAsync(updateItemDTO.ItemId);
                if (Item == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Item not found"
                    };
                }
                Item.ItemName = updateItemDTO.ItemName;
                Item.Description = updateItemDTO.Description;
                Item.Unit = updateItemDTO.Unit;
                Item.VolumeM3 = updateItemDTO.VolumeM3;
                Item.WeightKg = updateItemDTO.WeightKg;
                Item.Quantity = updateItemDTO.Quantity;
                await _unitOfWork.ItemRepo.UpdateAsync(Item);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "update Item succes"
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
