using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TripDriver
    {
        public Guid TripDriverId { get; set; }
        public Guid TripId { get; set; }
        public Trip Trip { get; set; } = null!;

        // Nếu driver có tài khoản trong hệ thống
        public Guid? DriverId { get; set; }
        public User? Driver { get; set; }

        // Nếu driver là external (nhập tay)
        public string? ExternalDriverName { get; set; }
        public string? ExternalDriverLicense { get; set; }
        public string? ExternalDriverPhone { get; set; }
    }
}
