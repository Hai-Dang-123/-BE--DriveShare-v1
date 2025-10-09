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
        public string Version { get; set; }
        public ICollection<ReportTerm> ReportTerms { get; set; }
    }
}
