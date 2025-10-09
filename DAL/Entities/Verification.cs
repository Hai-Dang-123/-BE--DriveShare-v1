using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Verification
    {
        public Guid VerificationId { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; } = null!;
        public Guid? VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; } = null!;
        public DocumentType DocType { get; set; }
        // Firebase URL để staff xem
        public string FrontDocumentUrl { get; set; } = null!;
        public string BackDocumentUrl { get; set; } = null!;

        // Hash để gọi OCR
        public string? FrontVNPTFileHash { get; set; }
        public string? BackVNPTFileHash { get; set; }
        public VerificationStatus Status { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        // JSON trả về từ OCR/eKYC (để staff xem chi tiết)
        public string? RawResultJson { get; set; }
    }
}
