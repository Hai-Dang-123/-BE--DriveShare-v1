using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IReviewService
    {
        Task<ResponseDTO> CraeteReviewAsync(CreateReviewDTO createReviewDTO);
        Task<ResponseDTO> GetReviewByIdAsync(Guid reviewId);
        Task<ResponseDTO> GetAllReviewByIdAsync();
        Task<ResponseDTO> UpdateReviewAsync(UpdateReviewDTO updateReviewDTO);
        Task<ResponseDTO> DeleteReviewAsync(Guid reviewId);
        Task<ResponseDTO> GetReviewsByTouserIdAsync(Guid toUserId);
        //Task<ResponseDTO> GetReviewsByRelatedVehicleIdAsync(Guid vehicleId);
        //Task<ResponseDTO> GetReviewsByRelatedItemIdAsync(Guid itemId);
        //Task<ResponseDTO> GetAverageRatingByUserIdAsync(Guid userId);
        //Task<ResponseDTO> GetAverageRatingByRelatedVehicleIdAsync(Guid vehicleId);
        //Task<ResponseDTO> GetAverageRatingByRelatedItemIdAsync(Guid itemId);

    }
}
