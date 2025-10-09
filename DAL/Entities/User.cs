using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;   

        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public DateTime? LastLoginAt { get; set; }
        public UserStatus UserStatus { get; set; } = UserStatus.ACTIVE;
        public ICollection<Verification> Verifications { get; set; } = new List<Verification>();
        public ICollection<UserToken> Tokens { get; set; } = new List<UserToken>();
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<UserViolation> UserViolations { get; set; } = new List<UserViolation>();

    }
}
