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
        public User User { get; set; } = null!;
        public string ViolationType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime ViolationDate { get; set; }
        public Guid BookingId { get; set; }
        //public Booking Booking { get; set; } = null!;
        public ICollection<UserActivityLog> UserActivityLogs { get; set; } = new List<UserActivityLog>();

    }
}
