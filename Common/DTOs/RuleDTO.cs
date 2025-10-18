using Common.Enums;
using System;

namespace Common.DTOs
{
    public class RuleDTO
    {
        public Guid RuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public RuleCategory Category { get; set; }
        public decimal Value { get; set; }
        public RuleUnit Unit { get; set; }
        public string? AppliedScopeType { get; set; }
        public Guid? AppliedScopeId { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public RuleStatus Status { get; set; }
    }

    public class CreateRuleDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public RuleCategory Category { get; set; }
        public decimal Value { get; set; }
        public RuleUnit Unit { get; set; }
        public string? AppliedScopeType { get; set; }
        public Guid? AppliedScopeId { get; set; }
        public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;
        public DateTime? EffectiveTo { get; set; }
    }

    public class UpdateRuleDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public RuleCategory? Category { get; set; }
        public decimal? Value { get; set; }
        public RuleUnit? Unit { get; set; }
        public string? AppliedScopeType { get; set; }
        public Guid? AppliedScopeId { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public RuleStatus? Status { get; set; }
    }
}
