using System;
using System.Collections.Generic;

namespace Common.DTOs
{
    public class ReportTemplateDTO
    {
        public Guid ReportTemplateId { get; set; }
        public string Version { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<ReportTermDTO> ReportTerms { get; set; } = new();
    }

    public class CreateReportTemplateDTO
    {
        public string Version { get; set; } = string.Empty;
        public List<CreateReportTermDTO> ReportTerms { get; set; } = new();
    }

    public class UpdateReportTemplateDTO
    {
        public string? Version { get; set; }
        public List<CreateReportTermDTO> ReportTerms { get; set; } = new();
    }

    public class ReportTermDTO
    {
        public Guid ReportTermId { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsMandatory { get; set; }
    }

    public class CreateReportTermDTO
    {
        public string Content { get; set; } = string.Empty;
        public bool IsMandatory { get; set; }
    }
}
