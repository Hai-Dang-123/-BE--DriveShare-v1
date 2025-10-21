using Common.Enums;
using System;

namespace Common.DTOs
{
    public class ItemBookingReportDTO
    {
        public Guid ReportId { get; set; }

        public string ReportTitle { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;   // ✅ string thay vì decimal

        public ReportType ReportType { get; set; }

        public ReportStatus Status { get; set; }

        public bool OwnerSigned { get; set; }

        public bool RenterSigned { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? SignedAt { get; set; }

        public Guid? ReportTemplateId { get; set; }

        public Guid ItemBookingId { get; set; }
    }

    public class CreateItemBookingReportDTO
    {
        public Guid ItemBookingId { get; set; }

        public string ReportTitle { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;   // ✅ string thay vì decimal

        public ReportType ReportType { get; set; }

        public Guid? ReportTemplateId { get; set; }
    }
}
