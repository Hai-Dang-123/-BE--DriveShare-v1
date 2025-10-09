using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class VehicleBooking
    {
        public Guid VehicleBookingId { get; set; }

        public Guid PostVehicleId { get; set; }
        public PostVehicle PostVehicle { get; set; } = null!;

        public Guid RenterId { get; set; }   // người thuê xe
        public User Renter { get; set; } = null!;

        public decimal TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookingStatus Status { get; set; }

        public Contract Contract { get; set; } = null!;
        public Guid ContractId { get; set; }
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }

}
