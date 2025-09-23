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
        public string RuleName { get; set; } = null!;
        public RuleCategory RuleCategory { get; set; } 
        public decimal RuleValue { get; set; } 
        public RuleUnit RuleUnit { get; set; } 
        //public string AppliedScope { get; set; } = null!; // role, vehicle_type, etc
        public DateTime EffectiveFrom { get; set; }
        //public DateTime? EffectiveTo { get; set; }
        public RuleStatus RuleStatus { get; set; }
    }
}
