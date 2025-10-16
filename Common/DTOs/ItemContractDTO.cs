using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ItemContractDTO
    {
    }
    public class CreateItemContractDto
    {
        public string version { get; set; } 
        public Guid ContractTemplateId { get; set; }
        public Guid ItemBookingId { get; set; }
    }

}
