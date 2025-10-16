using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTOs;
using Common.Enums;
using Common.Messages;
using Common.Settings;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class VerificationService : IVerificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseUploadService _firebaseUploadService;
        private readonly IEKYCService _ekycService;
        private readonly UserUtility _userUtility;
        private readonly ILogger<VerificationService> _logger;

        public VerificationService(
            IUnitOfWork unitOfWork,
            IFirebaseUploadService firebaseUploadService,
            IEKYCService ekycService,
            UserUtility userUtility,
            ILogger<VerificationService> logger)
        {
            _unitOfWork = unitOfWork;
            _firebaseUploadService = firebaseUploadService;
            _ekycService = ekycService;
            _userUtility = userUtility;
            _logger = logger;
        }

        // Step 1: Upload ảnh giấy tờ xe lên Firebase
        // ================================
        // VerificationService.cs
        // ================================
        public async Task<ResponseDTO> UploadDocumentsAsync(UploadVerificationDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 401, false);

            var vehicle = await _unitOfWork.VehicleRepo.GetByIdAsync(dto.VehicleId);
            if (vehicle == null)
                return new ResponseDTO("Vehicle not found", 404, false);

            // ✅ Upload ảnh lên Firebase
            var frontUrl = await _firebaseUploadService.UploadFileAsync(dto.FrontImage, userId, FirebaseFileType.VERIFICATION_IMAGES);
            string? backUrl = null;
            if (dto.BackImage != null)
                backUrl = await _firebaseUploadService.UploadFileAsync(dto.BackImage, userId, FirebaseFileType.VERIFICATION_IMAGES);

            // ✅ Upload lên VNPT eKYC và nhận hash
            var frontImageDTO = new EKYCUploadRequestDTO
            {
                File = dto.FrontImage,
                Title = "FrontImage",
                Description = "Vehicle document front image"
            };
            var frontHash = await _ekycService.UploadFileAsync(frontImageDTO);
            if (string.IsNullOrEmpty(frontHash))
                return new ResponseDTO("Failed to upload front image to OCR service", 500, false);

            string? backHash = null;
            if (dto.BackImage != null)
            {
                var backImageDTO = new EKYCUploadRequestDTO
                {
                    File = dto.BackImage,
                    Title = "BackImage",
                    Description = "Vehicle document back image"
                };
                backHash = await _ekycService.UploadFileAsync(backImageDTO);
                if (string.IsNullOrEmpty(backHash))
                    return new ResponseDTO("Failed to upload back image to OCR service", 500, false);
            }

            // ✅ Tạo bản ghi xác minh
            var verification = new Verification
            {
                VerificationId = Guid.NewGuid(),
                VehicleId = dto.VehicleId,
                DocumentType = dto.DocumentType,
                FrontDocumentUrl = frontUrl,
                BackDocumentUrl = backUrl,
                FrontServiceFileId = frontHash,
                BackServiceFileId = backHash,
                Status = VerificationStatus.PENDING,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.VerificationRepo.AddAsync(verification);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Upload success", 201, true, verification.VerificationId);
        }


        // Step 2: Gửi ảnh sang VNPT OCR để lấy thông tin
        public async Task<ResponseDTO> SendOcrRequestAsync(Guid verificationId)
        {
            var verification = await _unitOfWork.VerificationRepo.GetByIdAsync(verificationId);
            if (verification == null)
                return new ResponseDTO("Verification not found", 404, false);

            try
            {
                var ocrResult = await _ekycService.ReadVehicleDocumentAsync(
                    verification.FrontDocumentUrl, verification.BackDocumentUrl);

                verification.RawResultJson = ocrResult;
                await _unitOfWork.VerificationRepo.UpdateAsync(verification);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("OCR processed successfully", 200, true, ocrResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OCR request failed");
                return new ResponseDTO($"OCR error: {ex.Message}", 500, false);
            }
        }

        // Step 3: Lưu bản ghi Verification vào DB
        public async Task<ResponseDTO> CreateVerificationAsync(CreateVerificationDTO dto)
        {
            var vehicle = await _unitOfWork.VehicleRepo.GetByIdAsync(dto.VehicleId);
            if (vehicle == null)
                return new ResponseDTO("Vehicle not found", 404, false);

            var verification = new Verification
            {
                VerificationId = Guid.NewGuid(),
                VehicleId = dto.VehicleId,
                DocumentType = dto.DocumentType,
                FrontDocumentUrl = dto.FrontDocumentUrl,
                BackDocumentUrl = dto.BackDocumentUrl,
                RawResultJson = dto.RawResultJson,
                Status = VerificationStatus.PENDING,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.VerificationRepo.AddAsync(verification);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Verification created successfully", 201, true, verification.VerificationId);
        }

        // Get Verification theo VehicleId
        public async Task<ResponseDTO> GetVerificationByVehicleIdAsync(Guid vehicleId)
        {
            var data = await _unitOfWork.VerificationRepo.GetAllByListAsync(v => v.VehicleId == vehicleId);
            if (data == null || !data.Any())
                return new ResponseDTO("Verification not found", 404, false);
            var result = data.Select(v => new VerificationReadDTO
            {
                VerificationId = v.VerificationId,
                VehicleId = v.VehicleId ?? Guid.Empty,
                DocumentType = v.DocumentType,
                FrontDocumentUrl = v.FrontDocumentUrl,
                BackDocumentUrl = v.BackDocumentUrl,
                Status = v.Status,
                RawResultJson = v.RawResultJson,
                CreatedAt = v.CreatedAt
            }).ToList();

            return new ResponseDTO("Verification fetched successfully", 200, true, result);
        }

        public async Task<ResponseDTO> VerifyVehicleAsync(Guid verificationId, bool isApproved, string? note)
        {
            var verification = await _unitOfWork.VerificationRepo.GetByIdAsync(verificationId);
            if (verification == null)
                return new ResponseDTO("Verification not found", 404, false);

            verification.Status = isApproved ? VerificationStatus.APPROVED : VerificationStatus.REJECTED;
            verification.AdminNotes = note;
            verification.ProcessedAt = DateTime.UtcNow;

            await _unitOfWork.VerificationRepo.UpdateAsync(verification);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO(isApproved ? "Vehicle verified successfully" : "Verification rejected", 200, true);
        }
        // ✅ Step 5: Lấy trạng thái xác thực của người dùng hiện tại
        public async Task<ResponseDTO> GetMyVerificationStatusAsync()
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 401, false);

            // 🔹 Lấy danh sách xe mà user này sở hữu
            var vehicles = await _unitOfWork.VehicleRepo.GetAllByListAsync(v => v.OwnerUserId == userId);
            var vehicleIds = vehicles.Select(v => v.VehicleId).ToList();

            // 🔹 Lấy danh sách xác minh: bao gồm xác minh user & xác minh xe
            var verifications = await _unitOfWork.VerificationRepo.GetAllByListAsync(v =>
                v.UserId == userId || (v.VehicleId != null && vehicleIds.Contains(v.VehicleId.Value)));

            if (verifications == null || !verifications.Any())
                return new ResponseDTO("No verifications found for this user", 404, false);

            // 🔹 Map dữ liệu ra DTO
            var result = verifications.Select(v => new VerificationStatusReadDTO
            {
                VerificationId = v.VerificationId,
                DocType = v.DocumentType.ToString(),
                FrontDocumentUrl = v.FrontDocumentUrl,
                BackDocumentUrl = v.BackDocumentUrl,
                Status = v.Status.ToString(),
                Note = v.AdminNotes,
                CreatedAt = v.CreatedAt
            }).OrderByDescending(v => v.CreatedAt).ToList();

            return new ResponseDTO("Fetched verification status successfully", 200, true, result);
        }


    }

}
