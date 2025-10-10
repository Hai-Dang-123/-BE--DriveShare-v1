using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserActivityLog
    {
        public Guid UserActivityLogId { get; set; }
        public Guid UserId { get; set; } // Thêm UserId trực tiếp
        public User User { get; set; } = null!; // User thực hiện hành động

        public string Action { get; set; } = null!; // VD: "LOGIN", "CREATE_POST_VEHICLE", "UPDATE_PROFILE"
        public string? Description { get; set; } // Mô tả ngắn gọn
        public string? MetadataJson { get; set; } // Đổi Metadata thành MetadataJson để rõ là JSON
        public DateTime ActivityDate { get; set; } = DateTime.UtcNow;

        // Link đến UserViolation nếu hành động này liên quan đến một vi phạm
        public Guid? UserViolationId { get; set; }
        public UserViolation? UserViolation { get; set; }
    }
}
