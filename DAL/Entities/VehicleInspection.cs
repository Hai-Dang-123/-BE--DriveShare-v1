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

        // 🔗 Gắn với báo cáo cụ thể (biên bản bàn giao)
        public Guid ReportId { get; set; }
        public BaseReport Report { get; set; } = null!; // Có thể là VehicleBookingReport hoặc ItemBookingReport

        public Guid VehicleId { get; set; } // Trực tiếp link đến Vehicle
        public Vehicle Vehicle { get; set; } = null!;

        public InspectionType Type { get; set; } // Đổi InspectionType thành Type
        public string? ConditionNotes { get; set; }
        public string? EvidenceJson { get; set; } // JSON list URL ảnh/video hoặc checklist chi tiết
        public double OdometerReadingKm { get; set; } // Đổi OdometerReading thành OdometerReadingKm

        public InspectionStatus Status { get; set; }
        public DateTime InspectionDate { get; set; } = DateTime.UtcNow;

        // Nếu phát hiện vấn đề
        //public ICollection<InspectionResolution> Resolutions { get; set; } = new List<InspectionResolution>();
    }

}
