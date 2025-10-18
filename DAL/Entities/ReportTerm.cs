using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ReportTerm
    {
        public Guid ReportTermId { get; set; }
        public string Content { get; set; } = null!;
        public bool IsMandatory { get; set; }
        public Guid ReportTemplateId { get; set; }  
        public ReportTemplate ReportTemplate { get; set; } 


    }
}
