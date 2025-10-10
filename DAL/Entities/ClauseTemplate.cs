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
        public ICollection<ClauseContent> ClauseContents { get; set; } = new List<ClauseContent>();
        public ICollection<PostVehicle> Posts { get; set; } = new List<PostVehicle>();
    }

}
