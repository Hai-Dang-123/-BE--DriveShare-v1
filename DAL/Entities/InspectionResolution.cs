//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DAL.Entities
//{
//    public class InspectionResolution
//    {
//        public Guid Id { get; set; }
//        public Guid VehicleInspectionId { get; set; }
//        public VehicleInspection VehicleInspection { get; set; } = null!;
//        public Guid RaisedById { get; set; } // User
//        public User RaisedBy { get; set; } = null!;
//        public decimal CompensationAmount { get; set; }
//        public string? RenterResponse { get; set; }
//        public string? OwnerResponse { get; set; }
//        public InspectionStatus FinalStatus { get; set; }
//    }
//}
