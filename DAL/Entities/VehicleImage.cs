using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class VehicleImage
    {
        public Guid VehicleImageId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

    }
}
