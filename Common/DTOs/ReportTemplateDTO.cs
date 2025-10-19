using System;
using System.Collections.Generic;

namespace Common.DTOs
{
    public class CreateReportTemplateDTO
    {
        public string Version { get; set; } = null!;
        public List<ReportTermDTO>? ReportTerms { get; set; }
    }

    public class UpdateReportTemplateDTO
    {
        public string? Version { get; set; }
        public List<ReportTermDTO>? ReportTerms { get; set; }
    }

}
