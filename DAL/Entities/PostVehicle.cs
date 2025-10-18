using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PostVehicle
    {
        public Guid PostVehicleId { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        public Guid OwnerId { get; set; }
        public User Owner { get; set; } = null!; // User sở hữu xe này và đăng bài
        public decimal DailyPrice { get; set; }
        public PostStatus Status { get; set; }
        public string? Description { get; set; }
        public DateTime AvailableStartDate { get; set; } // Đổi tên để rõ nghĩa là ngày xe có sẵn
        public DateTime AvailableEndDate { get; set; } // Đổi tên để rõ nghĩa là ngày xe có sẵn

        // Liên kết với ClauseTemplate thay vì Clause
        public Guid ClauseTemplateId { get; set; }
        public ClauseTemplate ClauseTemplate { get; set; } = null!;

        public ICollection<AddOption> AddOptions { get; set; } = new List<AddOption>();
        public ICollection<VehicleBooking> VehicleBookings { get; set; } = new List<VehicleBooking>();
    }
}
