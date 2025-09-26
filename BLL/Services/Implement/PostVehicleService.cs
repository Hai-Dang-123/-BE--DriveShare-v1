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
        // delete bài post ( status deleted ) owner vs staff -DONE
        // get all bài post của owner -DONE
        // get all bài post của driver
        // get all bài post with status Pendding( staff )
        // get bài post theo id  -DONE
        // change status bài post ( request status ) 

        public async Task<ResponseDTO> CreatePostVehicleAsync(CreateRequestPostVehicleDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if ( userId == Guid.Empty)
            {
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 401, false);
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
                PostVehicleId = Guid.NewGuid(),
                VehicleId = dto.VehicleId,
                OwnerId = userId,
                DailyPrice = dto.DailyPrice,
                Status = PostStatus.PENDING, 
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,               
            };
            try
            {
                await _unitOfWork.PostVehicleRepo.AddAsync(newPostVehicle);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex) {
                return new ResponseDTO(PostMessages.ERROR_OCCURRED, 500, false);
            
            }
            return new ResponseDTO(PostMessages.POST_CREATED_SUCCESS,201,true);
        }

        public async Task<ResponseDTO> DeletePostVehicleAsync(Guid postId)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 401, false);
            }
           
                var existingPost = await _unitOfWork.PostVehicleRepo.GetByIdAsync(postId);
                if (existingPost == null)
                {
                    return new ResponseDTO(PostMessages.POST_NOT_FOUND, 404, false);
                }
            if (existingPost.OwnerId != userId && existingPost.Owner.Role.RoleName != "Staff" && existingPost.Owner.Role.RoleName != "Admin")
            {
                return new ResponseDTO(PostMessages.FORBIDDEN, 403, false);
            }
            if (existingPost.Status == PostStatus.RENTED)
                {
                    return new ResponseDTO(PostMessages.POST_RENTED, 400, false);
                }
            try
            {
                existingPost.Status = PostStatus.DELETED;
                await _unitOfWork.PostVehicleRepo.UpdateAsync(existingPost);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO(PostMessages.POST_DELETED_SUCCESS, 200, true);
            }
            catch (Exception ex)
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

                posts.Select(p => new GetPostVeVehicleDTO
                {
                    DailyPrice = p.DailyPrice,
                    EndDate = p.EndDate,
                    StartDate = p.StartDate,
                    Status = p.Status,
                    VehicleBrand = p.Vehicle.Brand,
                    VehicleModel = p.Vehicle.Model,
                    VehicleType = p.Vehicle.VehicleType.VehicleTypeName,
                    PlateNumber = p.Vehicle.PlateNumber,
                });
                return new ResponseDTO(PostMessages.GET_ALL_POST_SUCCESS, 200, true, posts);
            }
            catch (Exception ex)
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
                var postVehicle = await _unitOfWork.PostVehicleRepo.GetByIdAsync(postId);
                if (postVehicle == null)
                {
                    return new ResponseDTO(PostMessages.POST_NOT_FOUND, 404, false);
                }
                var result = new GetPostVeVehicleDTO
                {
                    OwnerName = postVehicle.Owner.UserName,
                    OwnerPhone = postVehicle.Owner.PhoneNumber,
                    DailyPrice = postVehicle.DailyPrice,
                    Status = postVehicle.Status,
                    StartDate = postVehicle.StartDate,
                    EndDate = postVehicle.EndDate,
                    VehicleBrand = postVehicle.Vehicle.Brand,
                    VehicleModel = postVehicle.Vehicle.Model,
                    VehicleType = postVehicle.Vehicle.VehicleType.VehicleTypeName,
                    PlateNumber = postVehicle.Vehicle.PlateNumber,
                };
                return new ResponseDTO(PostMessages.GET_POST_SUCCESS, 200, true, result);
            }
            catch (Exception ex)
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
            if (existingPost.Status == PostStatus.RENTED || existingPost.Status == PostStatus.DELETED)
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
            existingPost.PostVehicleId = dto.PostVehicleId;
            existingPost.DailyPrice = dto.DailyPrice;
            existingPost.StartDate = dto.StartDate;
            existingPost.EndDate = dto.EndDate;

            if (existingPost.Status == PostStatus.APPROVED)
            {
                existingPost.Status = PostStatus.PENDING;
            }

            try
            {
                await _unitOfWork.PostVehicleRepo.UpdateAsync(existingPost);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO(PostMessages.POST_UPDATED_SUCCESS, 200, true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(PostMessages.ERROR_OCCURRED, 500, false);
            }
        }
    }
}
