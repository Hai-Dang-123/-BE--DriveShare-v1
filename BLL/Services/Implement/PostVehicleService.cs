using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTOs;
using Common.Enums;
using Common.Messages;
using DAL.Entities;
using DAL.Repositories.Interface;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class PostVehicleService : IPostVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        public PostVehicleService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }

        

        // update bài post ( status pending ) -DONE
        // delete bài post ( status deleted ) owner vs staff - DONE
        // get all bài post của owner -DONE
        // get all bài post của driver -DONE
        // get all bài post with status Pendding( staff ) -DONE
        // get bài post theo id  -DONE
        // change status bài post ( request status ) - DONE

        public async Task<ResponseDTO> CreatePostVehicleAsync(CreateRequestPostVehicleDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            //if (userId == Guid.Empty)
            //{
            //    return new ResponseDTO(UserMessages.UNAUTHORIZED, 401, false);
            //}

            var vehicle = await _unitOfWork.VehicleRepo.GetByIdAsync(dto.VehicleId);
            if (vehicle == null)
            {
                return new ResponseDTO(VehicleMessages.VEHICLE_NOT_FOUND, 404, false);
            }

            if (dto.StartDate.Date < DateTime.UtcNow.Date)
            {
                return new ResponseDTO("StartDate must be today or later.", 400, false);
            }

            if (dto.EndDate <= dto.StartDate)
            {
                return new ResponseDTO("EndDate must be greater than StartDate.", 400, false);
            }
            var newPostVehicle = new PostVehicle
            {
                ClauseTemplateId = dto.ClauseId,
                PostVehicleId = Guid.NewGuid(),
                VehicleId = dto.VehicleId,
                OwnerId = userId,
                DailyPrice = dto.DailyPrice,
                Status = PostStatus.DRAFT,
                AvailableStartDate = dto.StartDate,
                AvailableEndDate = dto.EndDate,
                AddOptions = new List<AddOption>()
            };
            if (dto.AddOptions != null && dto.AddOptions.Any())
            {
                foreach (var option in dto.AddOptions)
                {
                    newPostVehicle.AddOptions.Add(new AddOption
                    {
                        AddOptionId = Guid.NewGuid(),
                        Description = option.Description,
                        PostVehicleId = newPostVehicle.PostVehicleId
                    });
                }
            }
            
            try
            {
                await _unitOfWork.PostVehicleRepo.AddAsync(newPostVehicle);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                return new ResponseDTO(PostMessages.ERROR_OCCURRED, 500, false);

            }
            return new ResponseDTO(PostMessages.POST_CREATED_SUCCESS, 201, true);
        }

        public async Task<ResponseDTO> DeletePostVehicleAsync(Guid postId)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 401, false);
            }

            var existingPost = await _unitOfWork.PostVehicleRepo.GetPostByIdAsync(postId);
            if (existingPost == null)
            {
                return new ResponseDTO(PostMessages.POST_NOT_FOUND, 404, false);
            }
            if (existingPost.OwnerId != userId && existingPost.Owner.Role.RoleName != "Staff" && existingPost.Owner.Role.RoleName != "Admin")
            {
                return new ResponseDTO(PostMessages.FORBIDDEN, 403, false);
            }
            if (existingPost.Status == PostStatus.BOOKED)
            {
                return new ResponseDTO(PostMessages.POST_RENTED, 400, false);
            }

            existingPost.Status = PostStatus.DELETED;

            try
            {
                
                await _unitOfWork.PostVehicleRepo.UpdateAsync(existingPost);
                await _unitOfWork.SaveChangeAsync();
                
            }
            catch (Exception)
            {
                return new ResponseDTO(PostMessages.ERROR_OCCURRED, 500, false);
            }

            return new ResponseDTO(PostMessages.POST_DELETED_SUCCESS, 200, true);
        }

        public async Task<ResponseDTO> GetAllPostVehicleAsync()
        {
            try
            {
                var posts = await _unitOfWork.PostVehicleRepo.GetAllByPostAsync();

                var result = posts.Select(p => new GetPostVehicleDTO
                {
                    ClauseId = p.ClauseTemplateId,
                    DailyPrice = p.DailyPrice,
                    EndDate = p.AvailableEndDate,
                    OwnerName = p.Owner?.Username ?? "N/A",
                    OwnerPhone = p.Owner?.PhoneNumber ?? "N/A",
                    StartDate = p.AvailableStartDate,
                    Status = p.Status,
                    VehicleBrand = p.Vehicle?.Brand ?? "N/A",
                    VehicleModel = p.Vehicle?.Model ?? "N/A",
                    VehicleType = p.Vehicle?.VehicleType?.Name ?? "N/A",
                    PlateNumber = p.Vehicle?.PlateNumber ?? "N/A" ,
                    ImageUrls = p.Vehicle?.Images?.Select(img => img.ImageUrl).ToList()
                }).ToList();

                return new ResponseDTO(PostMessages.GET_ALL_POST_SUCCESS, 200, true, result);
            }
            catch (Exception)
            {
                return new ResponseDTO(PostMessages.ERROR_OCCURRED, 500, false);
            }
        }

        public async Task<ResponseDTO> GetAllPostVehiclesByStatusAsync(PostStatus postStatus)
        {
            try
            {
                var posts = await _unitOfWork.PostVehicleRepo.GetAllByStatusAsync(postStatus);

                var result = posts.Select(p => new GetPostVehicleDTO
                {
                    ClauseId = p.ClauseTemplateId,
                    DailyPrice = p.DailyPrice,
                    EndDate = p.AvailableEndDate,
                    OwnerName = p.Owner?.Username ?? "N/A",
                    OwnerPhone = p.Owner?.PhoneNumber ?? "N/A",
                    StartDate = p.AvailableStartDate,
                    Status = p.Status,
                    VehicleBrand = p.Vehicle?.Brand ?? "N/A",
                    VehicleModel = p.Vehicle?.Model ?? "N/A",
                    VehicleType = p.Vehicle?.VehicleType?.Name ?? "N/A",
                    PlateNumber = p.Vehicle?.PlateNumber ?? "N/A",
                    ImageUrls = p.Vehicle?.Images?.Select(img => img.ImageUrl).ToList()
                }).ToList();

                return new ResponseDTO(PostMessages.GET_ALL_POST_SUCCESS, 200, true, result);
            }
            catch (Exception)
            {
                return new ResponseDTO(PostMessages.ERROR_OCCURRED, 500, false);
            }
        }

        public async Task<ResponseDTO> GetAllPostVehiclesOwner()
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 401, false);
            }
            try
            {
                var posts = await _unitOfWork.PostVehicleRepo.GetAllByUserIdAsync(userId);

                var result = posts.Select(p => new GetPostVehicleDTO
                {
                    ClauseId = p.ClauseTemplateId,
                    DailyPrice = p.DailyPrice,
                    EndDate = p.AvailableEndDate,
                    OwnerName = p.Owner.Username,
                    OwnerPhone = p.Owner.PhoneNumber,
                    StartDate = p.AvailableStartDate,
                    Status = p.Status,
                    VehicleBrand = p.Vehicle.Brand,
                    VehicleModel = p.Vehicle.Model,
                    VehicleType = p.Vehicle.VehicleType.Name,
                    PlateNumber = p.Vehicle.PlateNumber,
                    ImageUrls = p.Vehicle.Images.Select(img => img.ImageUrl).ToList()
                }).ToList();

                return new ResponseDTO(PostMessages.GET_ALL_POST_SUCCESS, 200, true, result);
            }
            catch (Exception)
            {
                return new ResponseDTO(PostMessages.ERROR_OCCURRED, 500, false);
            }
        }

        public async Task<ResponseDTO> GetPostVehicleByIdAsync(Guid postId)
        {
            if (postId == Guid.Empty)
            {
                return new ResponseDTO("Post ID not found", 400, false);
            }
            try
            {
                var postVehicle = await _unitOfWork.PostVehicleRepo.GetPostByIdAsync(postId);
                if (postVehicle == null)
                {
                    return new ResponseDTO(PostMessages.POST_NOT_FOUND, 404, false);
                }
                var result = new GetPostVehicleDTO
                {   ClauseId = postVehicle.ClauseTemplateId,
                    OwnerName = postVehicle.Owner.Username,
                    OwnerPhone = postVehicle.Owner.PhoneNumber,
                    DailyPrice = postVehicle.DailyPrice,
                    Status = postVehicle.Status,
                    StartDate = postVehicle.AvailableStartDate,
                    EndDate = postVehicle.AvailableEndDate,
                    VehicleBrand = postVehicle.Vehicle.Brand,
                    VehicleModel = postVehicle.Vehicle.Model,
                    VehicleType = postVehicle.Vehicle.VehicleType.Name,
                    PlateNumber = postVehicle.Vehicle.PlateNumber,
                    ImageUrls = postVehicle.Vehicle.Images.Select(img => img.ImageUrl).ToList()
                };
                return new ResponseDTO(PostMessages.GET_POST_SUCCESS, 200, true, result);
            }
            catch (Exception)
            {
                return new ResponseDTO(PostMessages.ERROR_OCCURRED, 500, false);
            }
        }
        public async Task<ResponseDTO> UpdatePostVehicleAsync(UpdateRequestPostVehicleDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 401, false);
            }
            var existingPost = await _unitOfWork.PostVehicleRepo.GetByIdAsync(dto.PostVehicleId);
            if (existingPost == null)
            {
                return new ResponseDTO(PostMessages.POST_NOT_FOUND, 404, false);
            }
            if (existingPost.OwnerId != userId)
            {
                return new ResponseDTO(PostMessages.FORBIDDEN, 403, false);
            }
            if (existingPost.Status == PostStatus.BOOKED || existingPost.Status == PostStatus.DELETED)
            {
                return new ResponseDTO("Cannot update because post is already RENTED or DELETED.", 400, false);
            }
            var postVehicle = _unitOfWork.PostVehicleRepo.GetByIdAsync(dto.PostVehicleId);
            if (dto.StartDate.Date < DateTime.UtcNow.Date)
            {
                return new ResponseDTO("StartDate must be today or later.", 400, false);
            }
            if (dto.EndDate <= dto.StartDate)
            {
                return new ResponseDTO("EndDate must be greater than StartDate.", 400, false);
            }
            existingPost.ClauseTemplateId = dto.ClauseId;
            existingPost.PostVehicleId = dto.PostVehicleId;
            existingPost.DailyPrice = dto.DailyPrice;
            existingPost.AvailableStartDate = dto.StartDate;
            existingPost.AvailableEndDate = dto.EndDate;

            if (existingPost.Status == PostStatus.ACTIVE)
            {
                existingPost.Status = PostStatus.DRAFT;
            }

            try
            {
                await _unitOfWork.PostVehicleRepo.UpdateAsync(existingPost);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO(PostMessages.POST_UPDATED_SUCCESS, 200, true);
            }
            catch (Exception)
            {
                return new ResponseDTO(PostMessages.ERROR_OCCURRED, 500, false);
            }
        }
        public async Task<ResponseDTO> ChangePostVehicleStatusAsync(ChangeStatusPostVehicleDTO dto)
        {
            if (dto.PostVehicleId == Guid.Empty)
            {
                return new ResponseDTO("PostVehicleId is required.", 400, false);
            }

            try
            {
                var postVehicle = await _unitOfWork.PostVehicleRepo.GetByIdAsync(dto.PostVehicleId);
                if (postVehicle == null)
                {
                    return new ResponseDTO(PostMessages.POST_NOT_FOUND, 404, false);
                }

                // Nếu đã bị xóa hoặc đang thuê thì không cho đổi trạng thái
                if (postVehicle.Status == PostStatus.DELETED || postVehicle.Status == PostStatus.BOOKED)
                {
                    return new ResponseDTO(PostMessages.POST_RENTED, 400, false);
                }

                postVehicle.Status = dto.Status;

                await _unitOfWork.PostVehicleRepo.UpdateAsync(postVehicle);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO(PostMessages.POST_UPDATED_SUCCESS, 200, true);
            }
            catch (Exception)
            {
                return new ResponseDTO(PostMessages.ERROR_OCCURRED, 500, false);
            }
        }

    }
}