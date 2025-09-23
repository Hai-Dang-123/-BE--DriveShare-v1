using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ContractTemplate
    {
        public Guid ContractTemplateId { get; set; }
        public string Version { get; set; } = null!;
        public ICollection<ContractTerm> ContractTerm { get; set; } = new List<ContractTerm>();
    }

}
