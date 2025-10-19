using Common.Enums;
using Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PostItem
    {
        public Guid PostItemId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal PricePerUnit { get; set; }
        public PostItemShippingRoute Route { get; set; } = new();
        public Item Item { get; set; } = new();
        public Guid ItemId { get; set; }
        public bool IsAvailable { get; set; }
        public PostItemStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<ItemBooking> ItemBookings { get; set; } = new List<ItemBooking>();
        public Guid ClauseTemplateId { get; set; }
        public ClauseTemplate ClauseTemplate { get; set; } = null!;
    }

}
