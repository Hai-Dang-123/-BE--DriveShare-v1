using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public abstract class BaseReport
    {
        public Guid ReportId { get; set; }
        public string ReportTitle { get; set; } = null!; // Đổi tên từ ReportName
        public ReportType ReportType { get; set; } // Enum (Handover, Incident, Return, etc.)
        public string Version { get; set; } = null!;
        public bool OwnerSigned { get; set; }
        public bool RenterSigned { get; set; } // Hoặc DriverSigned, ItemOwnerSigned tùy ngữ cảnh
        public ReportStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SignedAt { get; set; }
        public Guid ReportTemplateId { get; set; }
        public ReportTemplate ReportTemplate { get; set; } = null!;

        public ICollection<VehicleInspection> VehicleInspections { get; set; } = new List<VehicleInspection>();
        // Có thể thêm ICollection<ItemDamageReport> nếu bạn muốn chi tiết hư hỏng cho hàng hóa
    }
}
