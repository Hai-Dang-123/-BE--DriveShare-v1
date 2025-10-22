using System;
using Common.Enums;

namespace Common.DTOs
{
    public class ItemContractDTO
    {
        public Guid ContractId { get; set; }
        public string Version { get; set; } = string.Empty;
        public ContractStatus Status { get; set; }
        public bool OwnerSigned { get; set; }
        public bool RenterSigned { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? SignedAt { get; set; }
        public Guid ItemBookingId { get; set; }
    }

    public class CreateItemContractDTO
    {
        public string Version { get; set; } = string.Empty;
        public Guid ContractTemplateId { get; set; }
        public Guid ItemBookingId { get; set; }
    }
}
