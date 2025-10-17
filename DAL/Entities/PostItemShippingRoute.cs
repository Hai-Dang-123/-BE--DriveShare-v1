using Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PostItemShippingRoute
    {
        public Location StartLocation { get; set; } = null!;
        public Location EndLocation { get; set; } = null!;
        public DateTime ExpectedPickupDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public TimeWindow PickupTimeWindow { get; set; } = new(null, null);
        public TimeWindow DeliveryTimeWindow { get; set; } = new(null, null);
        public Guid PostItemId { get; set; }
        public PostItem PostItem { get; set; } = null!;
    }
}
