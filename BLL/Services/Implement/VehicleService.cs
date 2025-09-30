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
        private readonly FirebaseUploadService _firebaseUpload;


        public VehicleService(IUnitOfWork unitOfWork, UserUtility userUtility, FirebaseUploadService firebaseUpload)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
            _firebaseUpload = firebaseUpload;
        }

        // upload images và verification lên firebase

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

            var newVehicle = new Vehicle
            {
                VehicleId = Guid.NewGuid(),
                PlateNumber = dto.PlateNumber.Trim(),
                Model = dto.Model.Trim(),
                Brand = dto.Brand.Trim(),
                VehicleTypeId = dto.VehicleTypeId,
                UserId = userId,
                Status = VehicleStatus.ACTIVE,
            };

            try
            {
                await _unitOfWork.VehicleRepo.AddAsync(newVehicle);

                // Upload ảnh nếu có
                if (dto.Files != null && dto.Files.Any())
                {
                    foreach (var file in dto.Files)
                    {
                        var url = await _firebaseUpload.UploadFileAsync(file, userId, FirebaseFileType.VEHICLE_IMAGES);
                        var image = new VehicleImages
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

        public async Task<ResponseDTO> GetAllVehiclesAsync()
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 400, false);

            var vehicles = await _unitOfWork.VehicleRepo.GetAllByUserIdAsync(userId);
            if (vehicles == null || !vehicles.Any())
                return new ResponseDTO("No vehicles found", 404, false);

            var result = vehicles.Select(v => new VehicleReadDTO
            {
                VehicleId = v.VehicleId,
                PlateNumber = v.PlateNumber,
                Model = v.Model,
                Brand = v.Brand,
                VehicleTypeId = v.VehicleTypeId,
                UserId = v.UserId,
                Status = v.Status.ToString()
            });

            return new ResponseDTO("Vehicles retrieved successfully", 200, true, result);
        }

        // READ BY ID
        public async Task<ResponseDTO> GetVehicleByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return new ResponseDTO("Invalid vehicle id", 400, false);

            var vehicle = await _unitOfWork.VehicleRepo.GetByIdAsync(id);
            if (vehicle == null)
                return new ResponseDTO("Vehicle not found", 404, false);

            var dto = new VehicleReadDTO
            {
                VehicleId = vehicle.VehicleId,
                PlateNumber = vehicle.PlateNumber,
                Model = vehicle.Model,
                Brand = vehicle.Brand,
                VehicleTypeId = vehicle.VehicleTypeId,
                UserId = vehicle.UserId,
                Status = vehicle.Status.ToString()
            };

            return new ResponseDTO("Vehicle retrieved successfully", 200, true, dto);
        }

        // UPDATE
        public async Task<ResponseDTO> UpdateVehicleAsync(UpdateVehicleDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 400, false);

            var vehicle = await _unitOfWork.VehicleRepo.GetByIdAsync(dto.VehicleId);
            if (vehicle == null)
                return new ResponseDTO("Vehicle not found", 404, false);

            if (vehicle.UserId != userId)
                return new ResponseDTO("You do not own this vehicle", 403, false);

            vehicle.PlateNumber = dto.PlateNumber.Trim();
            vehicle.Model = dto.Model.Trim();
            vehicle.Brand = dto.Brand.Trim();
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
                        var image = new VehicleImages
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

            if (vehicle.UserId != userId)
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
    }
}
