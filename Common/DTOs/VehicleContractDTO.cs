using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class VehicleContractDTO
    {
    }
    public class CreateVehicleContractDto
    {
        public string Version { get; set; }
        public Guid ContractTemplateId { get; set; }
        public Guid VehicleBookingId { get; set; }
    }

}
