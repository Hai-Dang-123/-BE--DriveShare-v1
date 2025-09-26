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

        // update bài post ( status pending )
        // delete bài post ( status deleted )
        // get all bài post của owner
        // get all bài post của driver
        // get bài post theo id
        // change status bài post ( request status )

        public async Task<ResponseDTO> CreatePostVehicleAsync(CreateRequestPostVehicleDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if ( userId == Guid.Empty)
            {
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 401, false);
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
    }
}
