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

        // Mối quan hệ với User hoặc Vehicle, chỉ một trong hai sẽ có giá trị
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public Guid? VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        public DocumentType DocumentType { get; set; } // Đổi DocType thành DocumentType

        // URL ảnh/tài liệu đã upload (Firebase, S3, v.v.)
        public string FrontDocumentUrl { get; set; } = null!;
        public string? BackDocumentUrl { get; set; } // Có thể không có mặt sau (VD: giấy phép lái xe cũ)

        // Hash/ID để gọi các dịch vụ OCR/eKYC (VNPT, FPT, v.v.)
        public string? FrontServiceFileId { get; set; } // Đổi FrontVNPTFileHash thành FrontServiceFileId (chung chung hơn)
        public string? BackServiceFileId { get; set; } // Đổi BackVNPTFileHash thành BackServiceFileId

        public VerificationStatus Status { get; set; } // (Pending, Approved, Rejected, NeedsMoreInfo)
        public string? AdminNotes { get; set; } // Đổi Note thành AdminNotes để rõ ràng
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; } // Thời điểm xác minh được xử lý (Approved/Rejected)
        public Guid? ProcessedByUserId { get; set; } // Ai đã xử lý xác minh này (User Admin/Staff)
        public User? ProcessedByUser { get; set; }

        // JSON kết quả trả về từ OCR/eKYC để lưu trữ chi tiết
        public string? RawResultJson { get; set; }
    }
}
