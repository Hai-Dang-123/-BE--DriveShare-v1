using Common.Enums;
using Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TripStepInPlan
    {
        public Guid TripStepInPlanId { get; set; }
        public Guid TripId { get; set; }
        public Trip Trip { get; set; } = null!;

        // Một bước trong chuyến đi có thể được giao cho một tài xế cụ thể trong chuyến đó
        public Guid TripDriverId { get; set; }
        public TripDriver TripDriver { get; set; } = null!; // Đổi Driver thành TripDriver để rõ ràng

        public int StepNumber { get; set; } // Thứ tự bước

        public Location StartLocation { get; set; } = null!;
        public Location EndLocation { get; set; } = null!;

        public double PlannedDistanceKm { get; set; } // Khoảng cách dự kiến cho bước này
        public double? ActualDistanceKm { get; set; }

        public DateTime PlannedArrivalTime { get; set; } // Thời gian đến dự kiến tại điểm kết thúc của bước này
        public DateTime? ActualArrivalTime { get; set; }

        public RoadType RoadType { get; set; }
        public TripStepStatus Status { get; set; }

        public string? Notes { get; set; } // Ghi chú cho bước này (ví dụ: điểm dừng nghỉ)
    }
}
