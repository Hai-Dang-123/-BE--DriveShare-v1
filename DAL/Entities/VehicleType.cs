using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class VehicleType
    {
        public Guid VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; } = null!;
        public ICollection<Vehicle> Vehicles { get; set; } = null!;
    }
}
