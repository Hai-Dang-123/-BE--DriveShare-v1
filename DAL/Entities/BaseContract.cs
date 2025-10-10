using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public abstract class BaseContract // Tạo một base class cho Contract
    {
        public Guid ContractId { get; set; }
        public string Version { get; set; } = null!; // Version của hợp đồng cụ thể
        public bool OwnerSigned { get; set; }
        public bool RenterSigned { get; set; }
        public ContractStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SignedAt { get; set; }

        // Tham chiếu đến ContractTemplate gốc
        public Guid ContractTemplateId { get; set; }
        public ContractTemplate ContractTemplate { get; set; } = null!;
    }
}
