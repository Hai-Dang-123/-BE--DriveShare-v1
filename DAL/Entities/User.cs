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
        public string Username { get; set; } = null!; // Đổi UserName thành Username (thường là để đăng nhập)
        public string? DisplayName { get; set; } // Tên hiển thị (có thể là FullName)
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!; // Mỗi User có một Role

        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = null!; // Mỗi User có một Wallet


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public UserStatus Status { get; set; } = UserStatus.ACTIVE; // Đổi UserStatus thành Status

        // Profile information
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }

        public ICollection<Verification> Verifications { get; set; } = new List<Verification>();
        public ICollection<UserToken> Tokens { get; set; } = new List<UserToken>();
        public ICollection<Vehicle> OwnedVehicles { get; set; } = new List<Vehicle>(); // Đổi Vehicles thành OwnedVehicles
        public ICollection<PostVehicle> CreatedPostVehicles { get; set; } = new List<PostVehicle>(); // Đổi PostVehicles
        public ICollection<PostItem> CreatedPostItems { get; set; } = new List<PostItem>(); // Đổi PostItems
        public ICollection<VehicleBooking> AsRenterBookings { get; set; } = new List<VehicleBooking>(); // Người thuê
        public ICollection<ItemBooking> AsItemDriverBookings { get; set; } = new List<ItemBooking>(); // Người vận chuyển hàng

        public ICollection<UserViolation> UserViolations { get; set; } = new List<UserViolation>();
        public ICollection<Review> ReviewsGiven { get; set; } = new List<Review>(); // Đánh giá do user này viết
        public ICollection<Review> ReviewsReceived { get; set; } = new List<Review>(); // Đánh giá về user này
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Trip> CreatedTrips { get; set; } = new List<Trip>(); // Các chuyến đi do user này tạo
        public ICollection<TripDriver> AssignedTrips { get; set; } = new List<TripDriver>(); // Các chuyến đi user này được gán làm driver
    }
}
