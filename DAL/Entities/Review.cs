using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid FromUserId { get; set; }
        public User FromUser { get; set; } = null!; // Người viết đánh giá
        public Guid ToUserId { get; set; }
        public User ToUser { get; set; } = null!; // Người được đánh giá
        public int Rating { get; set; } // 1-5 sao
        public string? Comment { get; set; }
        public ReviewCategory Category { get; set; } // VD: Vehicle, Renter, Owner, Driver
        public string? ResponseComment { get; set; } // Phản hồi từ người được đánh giá
        public DateTime CreatedAt { get; set; }

        // Thêm các ID liên quan để biết review này về cái gì
        public Guid? RelatedVehicleBookingId { get; set; }
        public VehicleBooking? RelatedVehicleBooking { get; set; }

        public Guid? RelatedItemBookingId { get; set; }
        public ItemBooking? RelatedItemBooking { get; set; }

        public Guid? RelatedVehicleId { get; set; } // Đánh giá về xe nói chung, không phải booking cụ thể
        public Vehicle? RelatedVehicle { get; set; }
    }

}
