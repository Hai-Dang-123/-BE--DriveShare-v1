using Common.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Booking
    {
        public Guid BookingId { get; set; }
        public Guid PostVehicleId { get; set; }
        public PostVehicle PostVehicle { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public bool Confirmed { get; set; }
        public BookingStatus Status { get; set; }
        //public ICollection<VehicleInspection> VehicleInspections { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
