using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Trip
    {
        public Guid TripId { get; set; }
        public Guid CreateById { get; set; }
        public User CreateBy { get; set; } = null!;
        public string StartLocation { get; set; } = null!;
        public string EndLocation { get; set; } = null!;
        public double PlannedDistanceKm { get; set; }

        public double? ActualDistanceKm { get; set; }
        //public int DriverCount { get; set; }
        public DateTime ETA { get; set; }
        public TripStatus Status { get; set; } 
        public DateTime CreatedAt { get; set; }
        public ICollection<TripStepInPlan>? TripPlans { get; set; } = new List<TripStepInPlan>();
        public ICollection<TripDriver> Drivers { get; set; } = new List<TripDriver>();
    }
}
