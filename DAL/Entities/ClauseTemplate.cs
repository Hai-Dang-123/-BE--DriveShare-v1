using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ClauseTemplate
    {
        public Guid ClauseId { get; set; }
        public string Version { get; set; } = null!;
        public string Title { get; set; } = null!;
        public ClauseTemplateStatus Status { get; set; }
        public ICollection<ClauseTerm> Terms { get; set; } = new List<ClauseTerm>();

        public ICollection<PostVehicle> PostVehicles { get; set; } = new List<PostVehicle>();
        public ICollection<PostItem> PostItems { get; set; } = new List<PostItem>();
    }

}
