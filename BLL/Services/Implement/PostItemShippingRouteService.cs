using Common.DTOs;
using Common.ValueObjects;
using DAL.Entities;
using DAL.UnitOfWork;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class PostItemShippingRouteService : IPostItemShippingRouteService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostItemShippingRouteService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDTO> CreatePostItemShippingRouteAsync(CreatePostItemShippingRouteRequest request)
        {
            try
            {
                var postItem = await _unitOfWork.PostItemRepo.GetByIdAsync(request.PostItemId);
                if (postItem == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "PostItem not found"
                    };
                }

                // khởi tạo Value Object
                var startLocation = new Location(
                    request.StartLocationAddress,
                    request.StartLocationLatitude,
                    request.StartLocationLongitude
                );

                var endLocation = new Location(
                    request.EndLocationAddress,
                    request.EndLocationLatitude,
                    request.EndLocationLongitude
                );

                var pickupTimeWindow = new TimeWindow(
                    request.PickupTimeWindowStart,
                    request.PickupTimeWindowEnd
                );

                var deliveryTimeWindow = new TimeWindow(
                    request.DeliveryTimeWindowStart,
                    request.DeliveryTimeWindowEnd
                );

                // khởi tạo entity
                var route = new PostItemShippingRoute
                {
                    PostItemId = request.PostItemId,
                    StartLocation = startLocation,
                    EndLocation = endLocation,
                    ExpectedPickupDate = request.ExpectedPickupDate,
                    ExpectedDeliveryDate = request.ExpectedDeliveryDate,
                    PickupTimeWindow = pickupTimeWindow,
                    DeliveryTimeWindow = deliveryTimeWindow
                };


                await _unitOfWork.PostItemShippingRouteRepo.AddAsync(route);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Post item shipping route created successfully",
                };
            }
            catch (Exception ex) 
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    
                };
            }

        }

        public async  Task<ResponseDTO> GetALLPostItemShippingRouteAsync()
        {
            try
            {
                var routes = await _unitOfWork.PostItemShippingRouteRepo.GetAllAsync();

                if (routes == null || !routes.Any())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No PostItemShippingRoutes found"
                    };
                }

                var routeDTOs = routes.Select(r => new PostItemShippingRouteResponse
                {
                    PostItemId = r.PostItemId,
                    StartLocation = r.StartLocation,
                    EndLocation = r.EndLocation,
                    ExpectedPickupDate = r.ExpectedPickupDate,
                    ExpectedDeliveryDate = r.ExpectedDeliveryDate,
                    PickupTimeWindow = r.PickupTimeWindow,
                    DeliveryTimeWindow = r.DeliveryTimeWindow
                }).ToList();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Retrieved all PostItemShippingRoutes successfully",
                    Result = routeDTOs
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

        public async Task<ResponseDTO> GetByIdPostItemShippingRouteAsync(Guid id)
        {
            try
            {
                var route = await _unitOfWork.PostItemShippingRouteRepo.GetByIdAsync(id);

                if (route == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "PostItemShippingRoute not found"
                    };
                }

                var routeDTO = new PostItemShippingRouteResponse
                {
                    PostItemId = route.PostItemId,
                    StartLocation = route.StartLocation,
                    EndLocation = route.EndLocation,
                    ExpectedPickupDate = route.ExpectedPickupDate,
                    ExpectedDeliveryDate = route.ExpectedDeliveryDate,
                    PickupTimeWindow = route.PickupTimeWindow,
                    DeliveryTimeWindow = route.DeliveryTimeWindow
                };

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Retrieved PostItemShippingRoute successfully",
                    Result = routeDTO
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

        public async Task<ResponseDTO> UpdatePostItemShippingRouteAsync(CreatePostItemShippingRouteRequest request)
        {
            try
            {
                var route = await _unitOfWork.PostItemShippingRouteRepo.GetByIdAsync(request.PostItemId);

                if (route == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "PostItemShippingRoute not found"
                    };
                }

                route.StartLocation = new Location(
                    request.StartLocationAddress,
                    request.StartLocationLatitude,
                    request.StartLocationLongitude
                );

                route.EndLocation = new Location(
                    request.EndLocationAddress,
                    request.EndLocationLatitude,
                    request.EndLocationLongitude
                );

                route.ExpectedPickupDate = request.ExpectedPickupDate;
                route.ExpectedDeliveryDate = request.ExpectedDeliveryDate;

                route.PickupTimeWindow = new TimeWindow(
                    request.PickupTimeWindowStart,
                    request.PickupTimeWindowEnd
                );

                route.DeliveryTimeWindow = new TimeWindow(
                    request.DeliveryTimeWindowStart,
                    request.DeliveryTimeWindowEnd
                );

                await _unitOfWork.PostItemShippingRouteRepo.UpdateAsync(route);
                await _unitOfWork.SaveChangeAsync();

                var updatedDTO = new PostItemShippingRouteResponse
                {
                    PostItemId = route.PostItemId,
                    StartLocation = route.StartLocation,
                    EndLocation = route.EndLocation,
                    ExpectedPickupDate = route.ExpectedPickupDate,
                    ExpectedDeliveryDate = route.ExpectedDeliveryDate,
                    PickupTimeWindow = route.PickupTimeWindow,
                    DeliveryTimeWindow = route.DeliveryTimeWindow
                };

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "PostItemShippingRoute updated successfully",
                    Result = updatedDTO
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
