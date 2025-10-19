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
        public PostVehicle PostVehicle { get; set; } = null!; // Bài đăng xe được thuê

        public Guid RenterUserId { get; set; } // Đổi RenterId thành RenterUserId
        public User RenterUser { get; set; } = null!;

        public decimal TotalPrice { get; set; }
        public DateTime RentalStartDate { get; set; } // Đổi StartDate
        public DateTime RentalEndDate { get; set; } // Đổi EndDate
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Một VehicleBooking sẽ có một VehicleContract
        public VehicleContract VehicleContract { get; set; } = null!;

        public ICollection<VehicleBookingReport> Reports { get; set; } = new List<VehicleBookingReport>();
    }

}
