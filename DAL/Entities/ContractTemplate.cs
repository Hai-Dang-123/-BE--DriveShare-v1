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
        public string Name { get; set; } = null!; // Tên template (vd: "Hợp đồng thuê xe tải")
        public string Version { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<ContractTerm> ContractTerms { get; set; } = new List<ContractTerm>(); // Đổi tên thành ContractTerms
    }

}
