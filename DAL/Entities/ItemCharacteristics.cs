using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ItemCharacteristics
    {
        public bool IsFragile { get; set; }
        public bool IsFlammable { get; set; }
        public bool IsPerishable { get; set; }
        public bool RequiresRefrigeration { get; set; }
        public bool IsOversized { get; set; }
        public bool IsHazardous { get; set; }
        public bool IsProhibited { get; set; }
        public bool RequiresInsurance { get; set; }
        public bool RequiresSpecialHandling { get; set; }
        public string? OtherRequirements { get; set; }
        public ItemCharacteristicsStatus Status { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; } = null!;
    }
}
