using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class VehicleInspection
    {
        public Guid VehicleInspectionId { get; set; }

        // 🔗 Gắn với booking cụ thể (chuyến thuê / giao hàng)
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        // 🔹 Giao xe hay trả xe
        public InspectionType InspectionType { get; set; }
        // Ví dụ: Handover (nhận xe), Return (trả xe)

        // 🔹 Ghi chú tình trạng xe
        public string? ConditionNotes { get; set; }

        // 🔹 Dữ liệu chứng cứ (ảnh, video, checklist, ...)
        // JSON hợp lý nếu bạn lưu base64 hoặc list URL
        public string? EvidenceJson { get; set; }

        // 🔹 Số km tại thời điểm kiểm tra
        public double OdometerReading { get; set; }

        // 🔹 Trạng thái kiểm tra: Pending / Approved / Disputed / Completed
        public InspectionStatus Status { get; set; }

        // 🔹 Nếu phát hiện vấn đề (ví dụ hư hỏng, thiếu nhiên liệu, …)
        //public ICollection<InspectionResolution> Resolutions { get; set; }
    }

}
