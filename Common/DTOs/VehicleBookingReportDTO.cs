using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class VehicleBookingReportDTO
    {
        public Guid ReportId { get; set; }
        public string ReportTitle { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public int ReportType { get; set; }  // Map từ Enum ReportType
        public int Status { get; set; }      // Map từ Enum ReportStatus
        public bool OwnerSigned { get; set; }
        public bool RenterSigned { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? SignedAt { get; set; }
        public Guid ReportTemplateId { get; set; }
        public Guid VehicleBookingId { get; set; }
    }

    public class CreateVehicleBookingReportDTO
    {
        public string ReportTitle { get; set; } = string.Empty;
        public int ReportType { get; set; }
        public string Version { get; set; } = string.Empty;
        public Guid ReportTemplateId { get; set; }
        public Guid VehicleBookingId { get; set; }
        public string ReportTitle { get; set; } = null!;
        public ReportType ReportType { get; set; }
        public string Version { get; set; } = null!;
    }
}
