using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ITemDTO
    {
    }
    public class CreateItemDTO
    {
        public string ItemName { get; set; } 
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal WeightKg { get; set; }
        public decimal VolumeM3 { get; set; }
    }
    public class UpdateItemDTO
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; } 
        public string? Description { get; set; } 
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal WeightKg { get; set; }
        public decimal VolumeM3 { get; set; }
    }
    public class ItemResponseDTO
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; } 
        public string? Description { get; set; } 
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal WeightKg { get; set; }
        public decimal VolumeM3 { get; set; }
        public ItemStatus status { get; set; }
        public ItemCharacteristicsDTO? Characteristics { get; set; }

    }
}
