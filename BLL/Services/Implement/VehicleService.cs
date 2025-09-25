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

        public async Task<ResponseDTO> CreateVehicleAsync(CreateVehicleDTO dto)
        {
            // ✅ 1. Check UserId từ token
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 400, false);
            }

            // ✅ 2. Validation input cơ bản
            if (string.IsNullOrWhiteSpace(dto.PlateNumber))
                return new ResponseDTO("Plate number is required", 400, false);

            if (dto.PlateNumber.Length > 20)
                return new ResponseDTO("Plate number cannot exceed 20 characters", 400, false);

            if (string.IsNullOrWhiteSpace(dto.Model))
                return new ResponseDTO("Model is required", 400, false);

            if (dto.Model.Length > 50)
                return new ResponseDTO("Model cannot exceed 50 characters", 400, false);

            if (string.IsNullOrWhiteSpace(dto.Brand))
                return new ResponseDTO("Brand is required", 400, false);

            if (dto.Brand.Length > 50)
                return new ResponseDTO("Brand cannot exceed 50 characters", 400, false);

            if (dto.VehicleTypeId == Guid.Empty)
                return new ResponseDTO("VehicleTypeId is required", 400, false);

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
    }
}
