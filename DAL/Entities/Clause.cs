using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Clause
    {
        public Guid ClauseId { get; set; }
        public string ClauseVersion { get; set; } = null!;
        public string ClauseContent { get; set; } = null!;

        public ICollection<PostVehicle> Posts { get; set; } = new List<PostVehicle>();
    }

}
