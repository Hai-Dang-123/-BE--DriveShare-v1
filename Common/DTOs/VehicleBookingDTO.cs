using System;
using Common.Enums;

namespace Common.DTOs
{
    public class VehicleBookingDTO
    {
        public Guid VehicleBookingId { get; set; }
        public Guid PostVehicleId { get; set; }
        public Guid RenterUserId { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public VehicleBasicDTO? VehicleInfo { get; set; }   
        public UserDTO? RenterInfo { get; set; }
    }
}
