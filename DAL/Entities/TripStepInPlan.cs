using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TripStepInPlan
    {
        public Guid TripStepInPlanId { get; set; }
        public Guid TripId { get; set; }
        public Trip Trip { get; set; } = null!;
        public Guid TripDriverId { get; set; }
        public TripDriver Driver { get; set; } = null!;
        public int StepNumber { get; set; }
        public string StartLocation { get; set; } = null!;
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public string EndLocation { get; set; } = null!;
        public double EndLatitude { get; set; }
        public double EndLongitude { get; set; }
        public double DistanceKm { get; set; }
        public DateTime ETA { get; set; }
        public RoadType RoadType { get; set; }
        //public Guid? SuggestedRestStationId { get; set; }
        //public RestStation? SuggestedRestStation { get; set; }

        public TripStepStatus Status { get; set; }


    }
}
