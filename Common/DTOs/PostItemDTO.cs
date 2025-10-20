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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
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

    }
}
