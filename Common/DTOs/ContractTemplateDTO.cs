using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ContractTemplateDTO
    {
        public string Version { get; set; }
        public List<ContractTermDTO>? Terms { get; set; }
    }
    public class ContractTermDTO
    {
        public string Content { get; set; }

    }

}
