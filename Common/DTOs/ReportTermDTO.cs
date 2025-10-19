using System;

namespace Common.DTOs
{
    public class ReportTermDTO
    {
        public Guid? ReportTermId { get; set; }
        public string Content { get; set; } = null!;
        public bool IsMandatory { get; set; }
        public Guid? ReportTemplateId { get; set; }
    }
}
