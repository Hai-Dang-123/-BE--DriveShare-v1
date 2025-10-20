using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ReviewDTO
    {
    }
    public class CreateReviewDTO
    {
        public Guid ToUserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public ReviewCategory reviewCategory { get; set; }
        public Guid? RelatedVehicleBookingId { get; set; }
        public Guid? RelatedItemBookingId { get; set; }
        public Guid? RelatedVehicleId { get; set; }
    }
    public class UpdateReviewDTO
     {
            public Guid ReviewId { get; set; }
            public Guid ToUserId { get; set; }
            public int Rating { get; set; }
            public string Comment { get; set; } = null!;
            public ReviewCategory reviewCategory { get; set; }
            public Guid? RelatedVehicleBookingId { get; set; }
            public Guid? RelatedItemBookingId { get; set; }
            public Guid? RelatedVehicleId { get; set; }
     }
    public class ReviewResponseDTO
    {
        public Guid ReviewId { get; set; }
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public ReviewCategory Category { get; set; }
       // public string? ResponseComment { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? RelatedVehicleBookingId { get; set; }
        public Guid? RelatedItemBookingId { get; set; }
        public Guid? RelatedVehicleId { get; set; }
    }

}
