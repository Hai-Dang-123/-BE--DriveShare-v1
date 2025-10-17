using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ClauseTermDTO
    {
        public Guid ClauseTemplateId { get; set; }
        public string Content { get; set; } = null!;
        public bool IsMandatory { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class UpdateClauseTermDTO
    {
        public Guid ClauseTermId { get; set; }
        public string Content { get; set; } = null!;
        public bool IsMandatory { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class ResponseClauseTermDTO
    {
        public Guid? ClauseTermId { get; set; }
        public Guid ClauseTemplateId { get; set; }
        public string Content { get; set; } = null!;
        public bool IsMandatory { get; set; }
        public int DisplayOrder { get; set; }
    }

}
