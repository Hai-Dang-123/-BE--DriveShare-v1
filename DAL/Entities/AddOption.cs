using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AddOption
    {
        public Guid AddOptionId { get; set; }
        public string Content { get; set; } = null!;
        public Guid PostVehicle { get; set; }
        public PostVehicle PostVehicles { get; set; }
    }
}
