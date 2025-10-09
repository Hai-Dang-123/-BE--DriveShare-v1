using Common.Enums;
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
        public User User { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public string StartLocation { get; set; } = null!;
        public string EndLocation { get; set; } = null!;
        public DateTime StartDate { get; set; }  // ngày hàng sẵn sàng
        public DateTime EndDate { get; set; }  // ngày cần giao hàng
        public TimeOnly? AvailableTime { get; set; }  // khung giờ hàng phải được giao (nếu có)
        public bool IsAvailable { get; set; }
        public PostItemStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // 🚛 Thông tin hàng hoá
        public string ItemName { get; set; } = null!;     // tên mặt hàng
        public int Quantity { get; set; }                 // số lượng (đơn vị nhỏ nhất)
        public string? Unit { get; set; }                 // ví dụ: "thùng", "kiện", "kg"
        public decimal? Weight { get; set; }              // trọng lượng ước tính (kg)
        public decimal? Volume { get; set; }              // thể tích (m3)

        // ✅ Các đặc tính hàng hóa (phục vụ lọc & cảnh báo)
        public bool IsFragile { get; set; }               // hàng dễ vỡ
        public bool IsFlammable { get; set; }             // hàng dễ cháy nổ
        public bool IsPerishable { get; set; }            // hàng dễ hỏng (thực phẩm, rau quả,…)
        public bool RequiresRefrigeration { get; set; }   // cần xe lạnh / điều hòa
        public bool IsOversized { get; set; }             // quá khổ, quá tải
        public bool IsHazardous { get; set; }             // hàng nguy hiểm (hoá chất,…)
        public bool IsProhibited { get; set; }            // hàng cấm (cảnh báo nghi ngờ)
        public bool RequiresInsurance { get; set; }       // yêu cầu bảo hiểm hàng hóa
        public bool RequiresSpecialHandling { get; set; } // cần xử lý đặc biệt (dỡ hàng, đóng kiện,…)
        public string? OtherRequirements { get; set; }    // yêu cầu khác (ghi chú tuỳ ý)


    }
}
