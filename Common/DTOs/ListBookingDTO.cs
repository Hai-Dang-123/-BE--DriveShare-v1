using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ListBookingDTO
    {
        public Guid BookingId { get; set; }
        public Guid PostVehicleId { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Confirmed { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
