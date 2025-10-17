using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ContracttermDTO
    {
        public Guid? ContractTemplateId { get; set; }
        public bool IsMandatory { get; set; }
        public string Content { get; set; }
    }
    public class ContracttermResponseDTO
    {
        public Guid? ContractTemplateId { get; set; }
        public bool IsMandatory { get; set; }
        public string Content { get; set; }
    }
    public class UpdateContracttermDTO
    {
        public Guid ContractTermId { get; set; }
        public Guid ContractTemplateId { get; set; }
        public bool IsMandatory { get; set; }
        public string Content { get; set; }
    }
}
