using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserViolation
    {
        public Guid UserViolationId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!; // User bị vi phạm

        public string ViolationType { get; set; } = null!; // VD: "LateReturn", "DamageToVehicle", "FraudAttempt"
        public string Description { get; set; } = null!;
        public DateTime ViolationDate { get; set; } = DateTime.UtcNow;

        // Liên kết rõ ràng với loại booking
        public Guid? VehicleBookingId { get; set; }
        public VehicleBooking? VehicleBooking { get; set; }

        public Guid? ItemBookingId { get; set; }
        public ItemBooking? ItemBooking { get; set; }

        public ICollection<UserActivityLog> RelatedActivityLogs { get; set; } = new List<UserActivityLog>(); // Đổi tên
    }
}
