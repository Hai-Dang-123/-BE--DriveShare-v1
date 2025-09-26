using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Verification
    {
        public Guid VerificationId { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; } = null!;
        public Guid? VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; } = null!;
        public DocumentType DocType { get; set; } 
        public string DocumentUrl { get; set; }
        public VerificationStatus Status { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
