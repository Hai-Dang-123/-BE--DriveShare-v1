using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Common.DTOs
{
    // Upload ảnh giấy tờ xe (Front / Back)
    public class UploadVerificationDTO
    {
        public Guid VehicleId { get; set; }
        public DocumentType DocumentType { get; set; }

        public IFormFile FrontImage { get; set; } = null!;
        public IFormFile? BackImage { get; set; }
    }

    // Kết quả OCR trả về
    public class OcrResultDTO
    {
        public string PlateNumber { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public string ChassisNumber { get; set; } = string.Empty;
        public string EngineNumber { get; set; } = string.Empty;
        public string RegistrationDate { get; set; } = string.Empty;
    }

    // Tạo bản ghi xác thực
    public class CreateVerificationDTO
    {
        public Guid VehicleId { get; set; }
        public DocumentType DocumentType { get; set; }

        public string FrontDocumentUrl { get; set; } = string.Empty;
        public string? BackDocumentUrl { get; set; }

        public string? RawResultJson { get; set; }
    }

    // Read DTO cho API GetById
    public class VerificationReadDTO
    {
        public Guid VerificationId { get; set; }
        public Guid VehicleId { get; set; }
        public DocumentType DocumentType { get; set; }
        public string FrontDocumentUrl { get; set; } = string.Empty;
        public string? BackDocumentUrl { get; set; }
        public VerificationStatus Status { get; set; }
        public string? AdminNotes { get; set; }
        public string? RawResultJson { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
