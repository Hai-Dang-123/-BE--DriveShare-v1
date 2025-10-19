using System;

namespace Common.DTOs
{
    public class CreateItemBookingReportDTO
    {
        public string ReportTitle { get; set; }
        public int ReportType { get; set; }
        public string Version { get; set; }
        public Guid ReportTemplateId { get; set; }
        public Guid ItemBookingId { get; set; }
    }
}
