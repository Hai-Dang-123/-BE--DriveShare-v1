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
    }
    public class CreateVehicleBookingReportDTO
    {
        public Guid VehicleBookingId { get; set; }
        public string ReportTitle { get; set; } = null!;
        public ReportType ReportType { get; set; }
        public string Version { get; set; } = null!;
    }
}
