//using Common.Enums;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DAL.Entities
//{
//    public class VehicleInspection
//    {
//        public Guid Id { get; set; }
//        public Guid BookingId { get; set; }
//        public Booking Booking { get; set; } = null!;
//        public InspectionType InspectionType { get; set; }
//        public string? ConditionNotes { get; set; }
//        public string? EvidenceJson { get; set; }
//        public double OdometerReading { get; set; }
//        public InspectionStatus Status { get; set; }
//        public ICollection<InspectionResolution> Resolutions { get; set; }
//    }
//}
