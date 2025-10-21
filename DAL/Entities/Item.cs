using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Item
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal WeightKg { get; set; }
        public decimal VolumeM3 { get; set; }
        public ItemStatus Status { get; set; }
        public ItemCharacteristics Characteristics { get; set; } = new ItemCharacteristics();
    }
}
