using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TripDriver
    {
        public Guid TripDriverId { get; set; }
        public Guid TripId { get; set; }
        public Trip Trip { get; set; } = null!;

        // Driver có thể là User trong hệ thống hoặc external
        public Guid? DriverUserId { get; set; } // Đổi DriverId thành DriverUserId
        public User? DriverUser { get; set; }

        public string? ExternalDriverName { get; set; }
        public string? ExternalDriverLicense { get; set; }
        public string? ExternalDriverPhone { get; set; }

        public bool IsPrimaryDriver { get; set; } // Nếu có nhiều tài xế, ai là chính
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
