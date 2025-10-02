using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PostVehicle
    {
        public Guid PostVehicleId { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        public Guid OwnerId { get; set; } 
        public User Owner { get; set; } = null!;
        public decimal DailyPrice { get; set; }
        public PostStatus Status { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid ClauseId { get; set; }   
        public Clause Clause { get; set; }
        public ICollection<ContractTerm> ContractTerms { get; set; } = new List<ContractTerm>();
    }
}
