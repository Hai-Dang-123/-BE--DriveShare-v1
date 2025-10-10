using Common.Enums;
using Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Trip
    {
        public Guid TripId { get; set; }
        public Guid CreatorUserId { get; set; } // Đổi CreateById thành CreatorUserId
        public User CreatorUser { get; set; } = null!;

        public string Title { get; set; } = null!; // Thêm tiêu đề cho chuyến đi
        public Location StartLocation { get; set; } = null!;
        public Location EndLocation { get; set; } = null!;

        public double PlannedDistanceKm { get; set; }
        public double? ActualDistanceKm { get; set; }
        public DateTime PlannedDepartureTime { get; set; } // Thời gian khởi hành dự kiến
        public DateTime PlannedArrivalTime { get; set; } // Thời gian đến dự kiến
        public DateTime? ActualDepartureTime { get; set; }
        public DateTime? ActualArrivalTime { get; set; }

        public TripStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Liên kết chuyến đi với booking (nếu có)
        public Guid? RelatedItemBookingId { get; set; }
        public ItemBooking? RelatedItemBooking { get; set; }

        public ICollection<TripStepInPlan> TripSteps { get; set; } = new List<TripStepInPlan>(); // Đổi tên thành TripSteps
        public ICollection<TripDriver> TripDrivers { get; set; } = new List<TripDriver>(); // Đổi tên thành TripDrivers
    }
}
