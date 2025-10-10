using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Entities
{
    public class Wallet
    {
        public Guid WalletId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string Currency { get; set; } = "VND"; // Thêm đơn vị tiền tệ

        // Balance có thể được tính toán (computed property) hoặc lưu trữ và cập nhật qua trigger/logic
        // Tùy vào yêu cầu hiệu năng, nếu truy vấn thường xuyên thì nên lưu.
        public decimal CurrentBalance { get; set; } = 0; // Đổi Balance thành CurrentBalance
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
