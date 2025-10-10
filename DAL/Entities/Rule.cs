using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Rule
    {
        public Guid RuleId { get; set; }
        public string Name { get; set; } = null!; // Đổi RuleName thành Name
        public string Description { get; set; } = null!; // Mô tả chi tiết quy tắc

        public RuleCategory Category { get; set; }
        public decimal Value { get; set; } // Đổi RuleValue thành Value
        public RuleUnit Unit { get; set; } // Đổi RuleUnit thành Unit

        // --- Đã đưa AppliedScope trở lại và chi tiết hơn ---
        // Rule có thể áp dụng cho Role, VehicleType, SpecificUser, SpecificVehicle, etc.
        // Cách linh hoạt nhất là dùng JSON hoặc entity riêng, nhưng ở đây dùng string/Guid
        public string? AppliedScopeType { get; set; } // VD: "Role", "VehicleType", "User"
        public Guid? AppliedScopeId { get; set; } // ID của Role, VehicleType, User,... nếu AppliedScopeType không null

        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; } // Quy tắc có thể có thời hạn
        public RuleStatus Status { get; set; } // Enum (Active, Inactive)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
