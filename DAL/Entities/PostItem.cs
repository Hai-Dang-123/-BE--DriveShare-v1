using Common.Enums;
using Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PostItem
    {
        public Guid PostItemId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!; // User đăng bài này (người có hàng cần vận chuyển)

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal PricePerUnit { get; set; } // Giá vận chuyển/đơn vị hoặc tổng giá
        public Location StartLocation { get; set; } = null!;
        public Location EndLocation { get; set; } = null!;

        public DateTime ExpectedPickupDate { get; set; } // Ngày hàng sẵn sàng để lấy
        public DateTime ExpectedDeliveryDate { get; set; } // Ngày cần giao hàng

        // Khoảng thời gian cụ thể trong ngày (ví dụ: 09:00 - 17:00)
        public TimeWindow PickupTimeWindow { get; set; } = new TimeWindow(null, null); // Khởi tạo với null
        public TimeWindow DeliveryTimeWindow { get; set; } = new TimeWindow(null, null);

        public bool IsAvailable { get; set; }
        public PostItemStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // 🚛 Thông tin hàng hoá
        public string ItemName { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal WeightKg { get; set; } // Đổi tên rõ ràng đơn vị
        public decimal VolumeM3 { get; set; } // Đổi tên rõ ràng đơn vị

        // ✅ Các đặc tính hàng hóa (phục vụ lọc & cảnh báo)
        public bool IsFragile { get; set; }
        public bool IsFlammable { get; set; }
        public bool IsPerishable { get; set; }
        public bool RequiresRefrigeration { get; set; }
        public bool IsOversized { get; set; }
        public bool IsHazardous { get; set; }
        public bool IsProhibited { get; set; }
        public bool RequiresInsurance { get; set; }
        public bool RequiresSpecialHandling { get; set; }
        public string? OtherRequirements { get; set; }

        public ICollection<ItemBooking> ItemBookings { get; set; } = new List<ItemBooking>();
    }
}
