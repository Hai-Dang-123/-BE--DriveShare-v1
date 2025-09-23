using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserToken
    {
        public Guid UserTokenId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public TokenType TokenType { get; set; }
        public string TokenValue { get; set; } = null!;
        public bool IsRevoked { get; set; }
        public DateTime ExpiredAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
