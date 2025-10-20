using BLL.Services.Interface;
using BLL.Utilities;
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
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _User;
        public ReviewService(IUnitOfWork unitOfWork, UserUtility user)
        {
            _unitOfWork = unitOfWork;
            _User = user;
        }

        public async Task<ResponseDTO> CraeteReviewAsync(CreateReviewDTO createReviewDTO)
        {
            try
            {
                var userId = _User.GetUserIdFromToken();
                if (userId == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Unauthorized"
                    };
                }
                var newReview = new Review
                {
                    ReviewId = Guid.NewGuid(),
                    FromUserId = userId,
                    ToUserId = createReviewDTO.ToUserId,
                    Rating = createReviewDTO.Rating,
                    Comment = createReviewDTO.Comment,
                    Category = createReviewDTO.reviewCategory,
                    CreatedAt = DateTime.UtcNow,
                    RelatedVehicleBookingId = createReviewDTO.RelatedVehicleBookingId,
                    RelatedItemBookingId = createReviewDTO.RelatedItemBookingId,
                    RelatedVehicleId = createReviewDTO.RelatedVehicleId
                };
                await _unitOfWork.ReviewRepo.AddAsync(newReview);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Review created successfully",

                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> DeleteReviewAsync(Guid reviewId)
        {
            try
            {
                var review = _unitOfWork.ReviewRepo.GetByIdAsync(reviewId);
                if (review == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Review not found"
                    };
                }
                await _unitOfWork.ReviewRepo.DeleteAsync(reviewId);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Review deleted successfully"
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

        public async Task<ResponseDTO> GetAllReviewByIdAsync()
        {
            try
            {
                var reviews = await _unitOfWork.ReviewRepo.GetAllCReviewAsync();
                if (reviews == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "No reviews found"
                    };
                }

                var reviewResponse = reviews.Select(r => new ReviewResponseDTO
                {
                    ReviewId = r.ReviewId,
                    FromUserId = r.FromUserId,
                    ToUserId = r.ToUserId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    Category = r.Category,
                    CreatedAt = r.CreatedAt,
                    RelatedVehicleBookingId = r.RelatedVehicleBookingId,
                    RelatedItemBookingId = r.RelatedItemBookingId,
                    RelatedVehicleId = r.RelatedVehicleId
                }).ToList();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Reviews retrieved successfully",
                    Result = reviewResponse
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

        public async Task<ResponseDTO> GetReviewByIdAsync(Guid reviewId)
        {
            try
            {
                var review = await _unitOfWork.ReviewRepo.GetByIdAsync(reviewId);
                if (review == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Review not found"
                    };
                }
                var reviewResponse = new ReviewResponseDTO
                {
                    ReviewId = review.ReviewId,
                    FromUserId = review.FromUserId,
                    ToUserId = review.ToUserId,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    Category = review.Category,
                    CreatedAt = review.CreatedAt,
                    RelatedVehicleBookingId = review.RelatedVehicleBookingId,
                    RelatedItemBookingId = review.RelatedItemBookingId,
                    RelatedVehicleId = review.RelatedVehicleId
                };
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Review retrieved successfully",
                    Result = reviewResponse
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

        public async Task<ResponseDTO> GetReviewsByTouserIdAsync(Guid toUserId)
        {
            try
            {
                var reviews = await _unitOfWork.ReviewRepo.GetReviewsByToUserIdAsync(toUserId);
                if (reviews == null || !reviews.Any())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No reviews found for the specified user"
                    };
                }
                var reviewResponses = reviews.Select(r => new ReviewResponseDTO
                {
                    ReviewId = r.ReviewId,
                    FromUserId = r.FromUserId,
                    ToUserId = r.ToUserId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    Category = r.Category,
                    CreatedAt = r.CreatedAt,
                    RelatedVehicleBookingId = r.RelatedVehicleBookingId,
                    RelatedItemBookingId = r.RelatedItemBookingId,
                    RelatedVehicleId = r.RelatedVehicleId
                }).ToList();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Reviews retrieved successfully",
                    Result = reviewResponses
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

        public async Task<ResponseDTO> UpdateReviewAsync(UpdateReviewDTO updateReviewDTO)
        {
            try
            {
                var review = await _unitOfWork.ReviewRepo.GetByIdAsync(updateReviewDTO.ReviewId);
                if (review == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Review not found"
                    };
                }
                review.ToUserId = updateReviewDTO.ToUserId;
                review.Rating = updateReviewDTO.Rating;
                review.Comment = updateReviewDTO.Comment;
                review.Category = updateReviewDTO.reviewCategory;
                review.RelatedVehicleBookingId = updateReviewDTO.RelatedVehicleBookingId;
                review.RelatedItemBookingId = updateReviewDTO.RelatedItemBookingId;
                review.RelatedVehicleId = updateReviewDTO.RelatedVehicleId;
                await _unitOfWork.ReviewRepo.UpdateAsync(review);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Review updated successfully"
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
