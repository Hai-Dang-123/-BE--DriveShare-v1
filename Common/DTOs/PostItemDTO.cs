using Common.Enums;
using Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class PostItemDTO
    {
    }

    public class CreatePostItemDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PricePerUnit { get; set; }
        public Guid ItemId { get; set; }
        public bool IsAvailable { get; set; }
        public Guid ClauseTemplateId { get; set; }
    }
    public class UpdatePostItemDTO
    {
        public Guid PostItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PricePerUnit { get; set; }
        public Guid ItemId { get; set; }
        public bool IsAvailable { get; set; }
        public PostItemStatus Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid ClauseTemplateId { get; set; }
    }

    public class PostItemResponseDTO
    {
        public Guid PostItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PricePerUnit { get; set; }
        public Guid ItemId { get; set; }
        public bool IsAvailable { get; set; }
        public PostItemStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid ClauseTemplateId { get; set; }
        public Guid UserId { get; set; }

    }
    public class PostItemResponseByIdDTO
    {
        public Guid PostItemId { get; set; }
        public Guid UserId { get; set; }
        public Guid? VehicleId { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal PricePerUnit { get; set; }
        public bool IsAvailable { get; set; }
        public PostItemStatus  Status { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Guid? ClauseTemplateId { get; set; }

        // Nested DTOs
        public ItemResponseDTO? Item { get; set; }
        public PostItemShippingRouteResponse ShippingRoute { get; set; }
    }

    public class ItemCharacteristicsDTO
    {
        public bool IsFragile { get; set; }
        public bool IsFlammable { get; set; }
        public bool IsPerishable { get; set; }
        public bool RequireRefrigeration { get; set; }
        public bool IsOversized { get; set; }
        public bool IsHazardous { get; set; }
        public bool IsProhibited { get; set; }
        public bool RequireInsurance { get; set; }
        public bool RequireSpecialHandling { get; set; }
        public string? OtherRequirements { get; set; }
        public ItemCharacteristicsStatus Status { get; set; } 
    }


}
