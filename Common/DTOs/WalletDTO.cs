using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class WalletDTO
    {
        public Guid WalletId { get; set; }
        public Guid UserId { get; set; }
        public decimal CurrentBalance { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
