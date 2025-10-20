using BLL.Services.Interface;
using BLL.Utilities;
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
    public class PostItemService : IPostItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDTO> CreatePostItemAsync(CreatePostItemDTO createPostItemDTO)
        {
            try
            {
                var postItem = new PostItem
                {
                    Title = createPostItemDTO.Title,
                    Description = createPostItemDTO.Description,
                    PricePerUnit = createPostItemDTO.PricePerUnit,
                    ItemId = createPostItemDTO.ItemId,
                    IsAvailable = createPostItemDTO.IsAvailable,
                    Status = PostItemStatus.DRAFT,
                    CreatedAt = createPostItemDTO.CreatedAt,
                    UpdatedAt = createPostItemDTO.UpdatedAt,
                    ClauseTemplateId = createPostItemDTO.ClauseTemplateId
                };
                await _unitOfWork.PostItemRepo.AddAsync(postItem);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Post item created successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while creating the post item: {ex.Message}"
                };
            }
            }

        public async Task<ResponseDTO> DeletePostItemAsync(Guid postItemId)
        {
            try
            {
                var postItem = await _unitOfWork.PostItemRepo.GetByIdAsync(postItemId);
                if (postItem == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Post item not found"
                    };
                }
                postItem.Status = PostItemStatus.DELETED;
                await _unitOfWork.PostItemRepo.UpdateAsync(postItem);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Post item deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while deleting the post item: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO> GetAllPostItemsAsync()
        {
            try
            {

                var postItems = await _unitOfWork.PostItemRepo.GetAllPostItemsAsync();
                if (postItems == null || !postItems.Any())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No post items found"
                    };
                }

                var postItemDTOs = postItems.Select(postItem => new PostItemResponseDTO
                {
                    PostItemId = postItem.PostItemId,
                    Title = postItem.Title,
                    Description = postItem.Description,
                    PricePerUnit = postItem.PricePerUnit,
                    ItemId = postItem.ItemId,
                    IsAvailable = postItem.IsAvailable,
                    Status = postItem.Status,
                    CreatedAt = postItem.CreatedAt,
                    UpdatedAt = postItem.UpdatedAt,
                    ClauseTemplateId = postItem.ClauseTemplateId
                }).ToList();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Get all post items successfully",
                    Result = postItemDTOs
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while retrieving post items: {ex.Message}"
                };
            }
            }

        public async Task<ResponseDTO> GetPostItemByIdAsync(Guid postItemId)
        {
            try
            {
                var postItem = await _unitOfWork.PostItemRepo.GetByIdAsync(postItemId);
                if (postItem == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Post item not found"
                    };
                }
                var postItemDTO = new PostItemResponseDTO
                {
                    PostItemId = postItem.PostItemId,
                    Title = postItem.Title,
                    Description = postItem.Description,
                    PricePerUnit = postItem.PricePerUnit,
                    ItemId = postItem.ItemId,
                    IsAvailable = postItem.IsAvailable,
                    Status = postItem.Status,
                    CreatedAt = postItem.CreatedAt,
                    UpdatedAt = postItem.UpdatedAt,
                    ClauseTemplateId = postItem.ClauseTemplateId
                };

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Get post item successfully",
                    Result = postItemDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while retrieving the post item: {ex.Message}"
                };
            }
            }

        public async Task<ResponseDTO> UpdatePostItemAsync(UpdatePostItemDTO updatePostItemDTO)
        {
          try
            {
                var postItem = await _unitOfWork.PostItemRepo.GetByIdAsync(updatePostItemDTO.PostItemId);
                if (postItem == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Post item not found"
                    };
                }
                postItem.Title = updatePostItemDTO.Title;
                postItem.Description = updatePostItemDTO.Description;
                postItem.PricePerUnit = updatePostItemDTO.PricePerUnit;
                postItem.IsAvailable = updatePostItemDTO.IsAvailable;
                postItem.UpdatedAt = updatePostItemDTO.UpdatedAt;
                await _unitOfWork.PostItemRepo.UpdateAsync(postItem);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Post item updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while updating the post item: {ex.Message}"
                };
            }
        }
    }

}
