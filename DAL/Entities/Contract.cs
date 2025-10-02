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
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;
        public string Version { get; set; } = null!;
        public bool OwnerSigned { get; set; }
        public bool RenterSigned { get; set; }

        // Tham chiếu đến Template gốc
        public Guid ContractTemplateId { get; set; }
        public ContractTemplate ContractTemplate { get; set; } = null!;
        public ICollection<ContractTerm> Terms { get; set; } = new List<ContractTerm>();    
        public ContractStatus Status { get; set; }
    }
}
