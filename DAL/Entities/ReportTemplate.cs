using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ReportTemplate
    {
        public Guid ReportTemplateId { get; set; }
        public string Version { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<ReportTerm> ReportTerms { get; set; } = new List<ReportTerm>();
    }
}
