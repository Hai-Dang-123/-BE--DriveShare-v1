using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ContractTerm
    {
        public Guid ContractTermId { get; set; }
        public Guid? ContractId { get; set; }
        public Contract? Contract { get; set; } = null!;

        // Một điều khoản có thể thuộc về template hoặc contract cụ thể
        public Guid? ContractTemplateId { get; set; }
        public ContractTemplate? ContractTemplate { get; set; }
        public TermType TermType { get; set; }
        public string Content { get; set; } = null!;
        public bool IsMandatory { get; set; }
        public Guid PostVehicleId { get; set; }
        public PostVehicle PostVehicle { get; set; } = null!;

    }
}
