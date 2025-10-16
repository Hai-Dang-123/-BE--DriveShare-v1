using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTOs;
using Common.Enums;
using DAL.Entities;
using DAL.UnitOfWork;
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
        private readonly UserUtility _userUtility;
        public PostItemService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }
        public async Task<ResponseDTO> CreatePostItemAsync(CreatePostItemDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO("Unauthorized", 401, false);
            }

            var newPostItem = new PostItem
            {
                PostItemId = Guid.NewGuid(),
                UserId = userId,
                Title = dto.Title,
                Description = dto.Description,
                PricePerUnit = dto.PricePerUnit,
                StartLocation = dto.StartLocation,
                EndLocation = dto.EndLocation,
                ExpectedPickupDate = dto.ExpectedPickupDate,
                ExpectedDeliveryDate = dto.ExpectedDeliveryDate,
                PickupTimeWindow = dto.PickupTimeWindow,
                DeliveryTimeWindow = dto.DeliveryTimeWindow,
                IsAvailable = false,
                Status = PostItemStatus.DRAFT,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ItemName = dto.ItemName,
                Quantity = dto.Quantity,
                Unit = dto.Unit,
                WeightKg = dto.WeightKg,
                VolumeM3 = dto.VolumeM3,
                IsFragile = dto.IsFragile,
                IsFlammable = dto.IsFlammable,
                IsPerishable = dto.IsPerishable,
                RequiresRefrigeration = dto.RequiresRefrigeration,
                IsHazardous = dto.IsHazardous,
                IsProhibited = dto.IsProhibited,
                RequiresInsurance = dto.RequiresInsurance,
                RequiresSpecialHandling = dto.RequiresSpecialHandling,
                OtherRequirements = dto.OtherRequirements
            };

            try
            {
                await _unitOfWork.PostItemRepo.AddAsync(newPostItem);
                await _unitOfWork.SaveChangeAsync();

            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error creating post item: {ex.Message}", 500, false);
            }
            return new ResponseDTO("Post item created successfully", 201, true);
        }
    }
}
