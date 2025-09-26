using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTOs;
using Common.Enums;
using Common.Messages;
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

        public VehicleService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }

        // upload images và verification lên firebase

        public async Task<ResponseDTO> CreateVehicleAsync(CreateVehicleDTO dto)
        {
            // ✅ 1. Check UserId từ token
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 400, false);
            }

            
            // ✅ 3. Check biển số xe trùng
            var existed = await _unitOfWork.VehicleRepo.FindByLicenseAsync(dto.PlateNumber);
            if (existed != null)
            {
                return new ResponseDTO(VehicleMessages.DUPLICATED_VEHICLE, 400, false);
            }

            // ✅ 4. Check VehicleType có tồn tại
            var vehicleType = await _unitOfWork.VehicleTypeRepo.GetByIdAsync(dto.VehicleTypeId);
            if (vehicleType == null)
            {
                return new ResponseDTO("Vehicle type not found", 404, false);
            }

            // ✅ 5. Tạo entity mới
            var newVehicle = new Vehicle
            {
                VehicleId = Guid.NewGuid(),
                PlateNumber = dto.PlateNumber.Trim(),
                Model = dto.Model.Trim(),
                Brand = dto.Brand.Trim(),
                VehicleTypeId = dto.VehicleTypeId,
                UserId = userId,
                Status = VehicleStatus.ACTIVE,
                VerificationId = null
            };

            try
            {
                await _unitOfWork.VehicleRepo.AddAsync(newVehicle);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                // TODO: log ex
                return new ResponseDTO($"{VehicleMessages.ERROR_OCCURRED}: {ex.Message}", 500, false);
            }

            return new ResponseDTO(VehicleMessages.VEHICLE_CREATED_SUCCESS, 201, true, newVehicle.VehicleId);
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

            if (dto.VehicleId == Guid.Empty)
                return new ResponseDTO("VehicleId is required", 400, false);

            //// input validation
            //if (string.IsNullOrWhiteSpace(dto.PlateNumber) || dto.PlateNumber.Length > 20)
            //    return new ResponseDTO("Invalid plate number", 400, false);
            //if (string.IsNullOrWhiteSpace(dto.Model) || dto.Model.Length > 50)
            //    return new ResponseDTO("Invalid model", 400, false);
            //if (string.IsNullOrWhiteSpace(dto.Brand) || dto.Brand.Length > 50)
            //    return new ResponseDTO("Invalid brand", 400, false);
            //if (dto.VehicleTypeId == Guid.Empty)
            //    return new ResponseDTO("Vehicle type is required", 400, false);

            var vehicle = await _unitOfWork.VehicleRepo.GetByIdAsync(dto.VehicleId);
            if (vehicle == null)
                return new ResponseDTO("Vehicle not found", 404, false);

            if (vehicle.UserId != userId)
                return new ResponseDTO("You do not own this vehicle", 403, false);

            // check trùng biển số
            var existed = await _unitOfWork.VehicleRepo.FindByLicenseAsync(dto.PlateNumber);
            if (existed != null && existed.VehicleId != dto.VehicleId)
                return new ResponseDTO(VehicleMessages.DUPLICATED_VEHICLE, 400, false);

            var type = await _unitOfWork.VehicleTypeRepo.GetByIdAsync(dto.VehicleTypeId);
            if (type == null)
                return new ResponseDTO("Vehicle type not found", 404, false);

            // update
            vehicle.PlateNumber = dto.PlateNumber.Trim();
            vehicle.Model = dto.Model.Trim();
            vehicle.Brand = dto.Brand.Trim();
            vehicle.VehicleTypeId = dto.VehicleTypeId;

            try
            {
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
