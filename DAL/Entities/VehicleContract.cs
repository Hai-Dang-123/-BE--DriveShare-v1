using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class VehicleContract : BaseContract
    {
        // Liên kết đến VehicleBooking (1-1)
        public Guid VehicleBookingId { get; set; }
        public VehicleBooking VehicleBooking { get; set; } = null!;
    }
}
