using BLL.Services.Interface;
using Common.DTOs;
using Common.Enums;
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
                    ItemId = Guid.NewGuid(),
                    Status = ItemStatus.ACTIVE,
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

        public async Task<ResponseDTO> DeleteItemAsync(Guid itemId)
        {
            var Item = await _unitOfWork.ItemRepo.GetByIdAsync(itemId);
            if (Item == null)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Item not found",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            Item.Status = ItemStatus.DELETED;
            await _unitOfWork.ItemRepo.UpdateAsync(Item);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseDTO
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Update Item Succes"
            };
        }

        public async Task<ResponseDTO> GetAllItemsAsync()
        {
            try
            {
                var items = await _unitOfWork.ItemRepo.GetAllItemsAsync();
                if (items == null || !items.Any())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No Items found"
                    };
                }
                var itemDTOs = items.Select(item => new ItemResponseDTO
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Description = item.Description,
                    Unit = item.Unit,
                    VolumeM3 = item.VolumeM3,
                    WeightKg = item.WeightKg,
                    Quantity = item.Quantity
                }).ToList();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Items retrieved successfully",
                    Result = itemDTOs
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

        public async Task<ResponseDTO> GetItemByIdAsync(Guid itemId)
        {
            try
            {
                var item = await _unitOfWork.ItemRepo.GetByIdAsync(itemId);
                if (item == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Item not found"
                    };
                }
                var itemDTO = new ItemResponseDTO
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Description = item.Description,
                    Unit = item.Unit,
                    VolumeM3 = item.VolumeM3,
                    WeightKg = item.WeightKg,
                    Quantity = item.Quantity
                };
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Item retrieved successfully",
                    Result = itemDTO
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
