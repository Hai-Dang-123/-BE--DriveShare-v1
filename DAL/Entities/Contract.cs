using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Contract
    {
        public Guid ContractId { get; set; }
       
        public string Version { get; set; } = null!;
        public bool OwnerSigned { get; set; }
        public bool RenterSigned { get; set; }

        // Tham chiếu đến Template gốc
        public Guid ContractTemplateId { get; set; }
        public ContractTemplate ContractTemplate { get; set; } = null!;
       
        public ContractStatus Status { get; set; }

        // Liên kết đến Booking (1-1)
        public Guid? VehicleBookingId { get; set; }
        public VehicleBooking? VehicleBooking { get; set; }

        public Guid? ItemBookingId { get; set; }
        public ItemBooking? ItemBooking { get; set; }
    }
}
