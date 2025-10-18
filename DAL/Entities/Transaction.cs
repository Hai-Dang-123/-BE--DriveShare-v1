using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = null!;
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Liên kết với các đối tượng nghiệp vụ liên quan
        public Guid? RelatedBookingId { get; set; } // VehicleBookingId hoặc ItemBookingId
        public string? RelatedBookingType { get; set; } // "VehicleBooking" hoặc "ItemBooking"
        public Guid? RelatedContractId { get; set; }
        public Guid? RelatedReportId { get; set; }
        public string? Description { get; set; } // Mô tả thêm về giao dịch (VD: "Thanh toán thuê xe ABC")
    }
}
