using Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class PostItemShippingRouteDTO
    {
    }
    public class CreatePostItemShippingRouteRequest
    {
        public Guid PostItemId { get; set; }

        public string StartLocationAddress { get; set; } = null!;
        public double StartLocationLatitude { get; set; }
        public double StartLocationLongitude { get; set; }

        public string EndLocationAddress { get; set; } = null!;
        public double EndLocationLatitude { get; set; }
        public double EndLocationLongitude { get; set; }

        public DateTime ExpectedPickupDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }

        public TimeOnly? PickupTimeWindowStart { get; set; }
        public TimeOnly? PickupTimeWindowEnd { get; set; }

        public TimeOnly? DeliveryTimeWindowStart { get; set; }
        public TimeOnly? DeliveryTimeWindowEnd { get; set; }
    }
    public class PostItemShippingRouteResponse
    {
        public Location StartLocation { get; set; } 
        public Location EndLocation { get; set; }
        public DateTime ExpectedPickupDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public TimeWindow PickupTimeWindow { get; set; } 
        public TimeWindow DeliveryTimeWindow { get; set; }
        public Guid PostItemId { get; set; }
    }

}
