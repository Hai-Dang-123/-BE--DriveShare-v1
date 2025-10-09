using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Report
    {
        public Guid ReportId { get; set; }
        public string ReportName { get; set; }
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }
        public ReportType ReportType { get; set; }
        public string Version { get; set; } = null!;
        public bool OwnerSigned { get; set; }
        public bool RenterSigned { get; set; }
        public Guid ReportTemplateId { get; set; }
        public ReportTemplate ReportTemplate { get; set; }
        public ICollection<VehicleInspection> VehicleInspections { get; set; } = new List<VehicleInspection>();


    }
}
