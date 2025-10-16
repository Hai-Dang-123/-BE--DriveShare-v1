using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ContractTemplateDTO
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public List<ContractTermDTO> Terms { get; set; }
    }
    public class ContractTemplateResponseDTO
    {
        public Guid ContractTemplateId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ContractTermResponseDTO> Terms { get; set; }

    }
    public class ContractTermDTO
    {
        public bool IsMandatory { get; set; }
        public string Content { get; set; }

    }
    public class ContractTermResponseDTO
    {
        public Guid? ContractTemplateId { get; set; }
        public Guid ContractTermId { get; set; }
        public bool IsMandatory { get; set; }
        public string Content { get; set; }
        
    }

}
