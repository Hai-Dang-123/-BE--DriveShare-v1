using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; }
        public string PlateNumber { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public Guid VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; } = null!;
        public Guid? VerificationId { get; set; }
        public Verification Verification { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public VehicleStatus Status { get; set; } = VehicleStatus.ACTIVE;

        public ICollection<PostVehicle> Posts { get; set; } = null!;
        public ICollection<VehicleImages> VehicleImages { get; set; } = null!;
       
    }
}
