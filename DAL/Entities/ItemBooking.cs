using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ItemBooking
    {
        public Guid ItemBookingId { get; set; }

        public Guid PostItemId { get; set; }
        public PostItem PostItem { get; set; } = null!;

        public Guid DriverId { get; set; }
        public User Driver { get; set; } = null!;

        public Guid VehicleId { get; set; }  // xe dùng để chở hàng (có thể là xe riêng hoặc thuê)
        public Vehicle Vehicle { get; set; } = null!;

        public string ItemBookingCode { get; set; } = null!; // Mã đặt chỗ

        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        // Một ItemBooking sẽ có một ItemContract
        public Guid ItemContractId { get; set; }
        public ItemContract ItemContract { get; set; } = null!;
        public ICollection<ItemBookingReport> Reports { get; set; } = new List<ItemBookingReport>();
    }

}
