using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class AddOptionDTO
    {
        public Guid AddOptionId { get; set; }
        public string Description { get; set; } = null!;
        public Guid PostVehicleId { get; set; }
    }
    public class CreateAddOptionDTO
    {
        public string Description { get; set; } = null!;
        public Guid PostVehicleId { get; set; }
    }
}
