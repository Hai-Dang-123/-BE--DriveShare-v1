using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class PostVehicleDTO
    {
     
    }
    public class CreateRequestPostVehicleDTO
    {
        public Guid VehicleId { get; set; }
        public decimal DailyPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       
    }
}
