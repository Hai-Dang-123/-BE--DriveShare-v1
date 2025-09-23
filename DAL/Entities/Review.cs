using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid FromUserId { get; set; }
        public User FromUser { get; set; } = null!;
        public Guid ToUserId { get; set; }
        public User ToUser { get; set; } = null!;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public ReviewCategory Category { get; set; }
        public string? ResponseComment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
