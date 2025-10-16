using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTOs;
using Common.Enums;
using Common.Messages;
using Common.Settings;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        private readonly IFirebaseUploadService _firebaseUpload;
        private readonly IEKYCService _ekycService;



        // miss - ekyc
        

        public VehicleService(IUnitOfWork unitOfWork, UserUtility userUtility, IFirebaseUploadService firebaseUpload,IEKYCService eKYCService)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
            _firebaseUpload = firebaseUpload;
            _ekycService = eKYCService;

        }

        // upload images và verification lên firebase

        // thêm images giấy tờ
        // ekyc upload giấy tờ trước - sau 
        // orc - trả về json
        // create verification 



        public async Task<ResponseDTO> CreateVehicleAsync(CreateVehicleDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 400, false);

            var existed = await _unitOfWork.VehicleRepo.FindByLicenseAsync(dto.PlateNumber);
            if (existed != null)
                return new ResponseDTO(VehicleMessages.DUPLICATED_VEHICLE, 400, false);

            var vehicleType = await _unitOfWork.VehicleTypeRepo.GetByIdAsync(dto.VehicleTypeId);
            if (vehicleType == null)
                return new ResponseDTO("Vehicle type not found", 404, false);

            // ✅ Thêm đầy đủ dữ liệu cần thiết
            var newVehicle = new Vehicle
            {
                VehicleId = Guid.NewGuid(),
                PlateNumber = dto.PlateNumber.Trim(),
                Model = dto.Model.Trim(),
                Brand = dto.Brand.Trim(),
                Color = dto.Color.Trim(), // ✅ thêm Color
                VehicleTypeId = dto.VehicleTypeId,
                OwnerUserId = userId,

                Status = VehicleStatus.ACTIVE,
                YearOfManufacture = dto.year,
                CreatedAt = DateTime.UtcNow

            };

            try
            {
                await _unitOfWork.VehicleRepo.AddAsync(newVehicle);

                // ✅ Upload ảnh nếu có
                if (dto.Files != null && dto.Files.Any())
                {
                    foreach (var file in dto.Files)
                    {
                        var url = await _firebaseUpload.UploadFileAsync(file, userId, FirebaseFileType.VEHICLE_IMAGES);
                        var image = new VehicleImage
                        {
                            VehicleImageId = Guid.NewGuid(),
                            VehicleId = newVehicle.VehicleId,
                            ImageUrl = url
                        };
                        await _unitOfWork.VehicleImagesRepo.AddAsync(image);

                   
                    }
                }

                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Vehicle created successfully", 201, true, newVehicle.VehicleId);
        }


        // GET ALL (có ảnh)
        public async Task<ResponseDTO> GetAllVehiclesAsync()
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 400, false);

            var vehicles = await _unitOfWork.VehicleRepo.GetAllWithImagesByUserIdAsync(userId);
            if (vehicles == null || !vehicles.Any())
                return new ResponseDTO("No vehicles found", 404, false);

            var result = vehicles.Select(v => new VehicleReadDTO
            {
                VehicleId = v.VehicleId,
                PlateNumber = v.PlateNumber,
                Model = v.Model,
                Brand = v.Brand,
                Color = v.Color,
                VehicleTypeId = v.VehicleTypeId,
                UserId = v.OwnerUserId,
                Status = v.Status.ToString(),
                ImageUrls = v.Images.Select(img => img.ImageUrl).ToList()
            });

            return new ResponseDTO("Vehicles retrieved successfully", 200, true, result);
        }

        // GET BY ID (có ảnh)
        public async Task<ResponseDTO> GetVehicleByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return new ResponseDTO("Invalid vehicle id", 400, false);

            var vehicle = await _unitOfWork.VehicleRepo.GetByIdWithImagesAsync(id);
            if (vehicle == null)
                return new ResponseDTO("Vehicle not found", 404, false);

            var dto = new VehicleReadDTO
            {
                VehicleId = vehicle.VehicleId,
                PlateNumber = vehicle.PlateNumber,
                Model = vehicle.Model,
                Brand = vehicle.Brand,
                Color = vehicle.Color,
                VehicleTypeId = vehicle.VehicleTypeId,
                UserId = vehicle.OwnerUserId,
                Status = vehicle.Status.ToString(),
                ImageUrls = vehicle.Images.Select(img => img.ImageUrl).ToList()
            };

            return new ResponseDTO("Vehicle retrieved successfully", 200, true, dto);
        }

        public async Task<ResponseDTO> UpdateVehicleAsync(UpdateVehicleDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 400, false);

            var vehicle = await _unitOfWork.VehicleRepo.GetByIdAsync(dto.VehicleId);
            if (vehicle == null)
                return new ResponseDTO("Vehicle not found", 404, false);

            if (vehicle.OwnerUserId != userId)
                return new ResponseDTO("You do not own this vehicle", 403, false);

            vehicle.PlateNumber = dto.PlateNumber.Trim();
            vehicle.Model = dto.Model.Trim();
            vehicle.Brand = dto.Brand.Trim();
            vehicle.Color = dto.Color.Trim();
            vehicle.VehicleTypeId = dto.VehicleTypeId;

            try
            {
                // Xoá ảnh cũ
                if (dto.DeletedImageIds != null && dto.DeletedImageIds.Any())
                {
                    foreach (var imgId in dto.DeletedImageIds)
                    {
                        var img = await _unitOfWork.VehicleImagesRepo.GetByIdAsync(imgId);
                        if (img != null && img.VehicleId == vehicle.VehicleId)
                        {
                            await _unitOfWork.VehicleImagesRepo.DeleteAsync(imgId);
                        }
                    }
                }

                // Upload ảnh mới
                if (dto.NewFiles != null && dto.NewFiles.Any())
                {
                    foreach (var file in dto.NewFiles)
                    {
                        var url = await _firebaseUpload.UploadFileAsync(file, userId, FirebaseFileType.VEHICLE_IMAGES);
                        var image = new VehicleImage
                        {
                            VehicleImageId = Guid.NewGuid(),
                            VehicleId = vehicle.VehicleId,
                            ImageUrl = url
                        };
                        await _unitOfWork.VehicleImagesRepo.AddAsync(image);
                    }
                }

                await _unitOfWork.VehicleRepo.UpdateAsync(vehicle);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Vehicle updated successfully", 200, true);
        }


        // DELETE (soft)
        public async Task<ResponseDTO> DeleteVehicleAsync(Guid id)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 400, false);

            if (id == Guid.Empty)
                return new ResponseDTO("VehicleId is required", 400, false);

            var vehicle = await _unitOfWork.VehicleRepo.GetByIdAsync(id);
            if (vehicle == null)
                return new ResponseDTO("Vehicle not found", 404, false);

            if (vehicle.OwnerUserId != userId)
                return new ResponseDTO("You do not own this vehicle", 403, false);

            vehicle.Status = VehicleStatus.DELETED;

            try
            {
                await _unitOfWork.VehicleRepo.UpdateAsync(vehicle);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Vehicle deleted successfully", 200, true);
        }

        public async Task<ResponseDTO> ChangeStatusAsync(ChangeVehicleStatusDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 400, false);

            var vehicle = await _unitOfWork.VehicleRepo.GetByIdAsync(dto.VehicleId);
            if (vehicle == null)
                return new ResponseDTO("Vehicle not found", 404, false);

            if (vehicle.OwnerUserId != userId)
                return new ResponseDTO("You do not own this vehicle", 403, false);

            vehicle.Status = dto.NewStatus;

            try
            {
                await _unitOfWork.VehicleRepo.UpdateAsync(vehicle);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error: {ex.Message}", 500, false);
            }

            return new ResponseDTO($"Vehicle status updated to {dto.NewStatus}", 200, true);
        }

        public async Task<ResponseDTO> GetVehicleDetailAsync(Guid id)
        {
            if (id == Guid.Empty)
                return new ResponseDTO("Invalid vehicle id", 400, false);

            var vehicle = await _unitOfWork.VehicleRepo.GetByIdWithFullDetailAsync(id);
            if (vehicle == null)
                return new ResponseDTO("Vehicle not found", 404, false);

            var post = vehicle.PostsForRent.FirstOrDefault(p => p.Status == PostStatus.ACTIVE);
            if (post == null)
                return new ResponseDTO("No active post found for this vehicle", 404, false);

            var dto = new VehicleDetailDTO
            {
                PostVehicleId = post.PostVehicleId,
                DailyPrice = post.DailyPrice,
                Description = post.Description ?? "Không có mô tả",
                Status = post.Status.ToString(),
                Vehicle = new VehicleBasicDTO
                {
                    VehicleId = vehicle.VehicleId,
                    Brand = vehicle.Brand,
                    Model = vehicle.Model,
                    VehicleTypeName = vehicle.VehicleType?.Name ?? "",
                    LicensePlate = vehicle.PlateNumber,
                    Color = vehicle.Color,
                    Images = vehicle.Images.Select(i => new VehicleImageDTO
                    {
                        ImageId = i.VehicleImageId,
                        ImageUrl = i.ImageUrl
                    }).ToList()
                },
                Owner = new OwnerDTO
                {
                    UserId = vehicle.OwnerUser.UserId,
                    Name = vehicle.OwnerUser.Username ,
                    Phone = vehicle.OwnerUser.PhoneNumber
                }
            };

            return new ResponseDTO("Lấy chi tiết xe thành công", 200, true, dto);
        }

    }
}
