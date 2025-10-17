using System;

namespace Common.DTOs
{
    public class ContractResponseDTO
    {
        public Guid ContractId { get; set; }
        public string Version { get; set; } = string.Empty;
        public bool OwnerSigned { get; set; }
        public bool RenterSigned { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid ContractTemplateId { get; set; }

        public Guid? VehicleBookingId { get; set; }
        public Guid? ItemBookingId { get; set; }

        // ✅ Thời gian tạo & ký
        public DateTime CreatedAt { get; set; }
        public DateTime? SignedAt { get; set; }
    }
}
