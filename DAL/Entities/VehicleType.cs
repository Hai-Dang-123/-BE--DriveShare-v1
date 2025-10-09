using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class VehicleType
    {
        public Guid VehicleTypeId { get; set; }
        public string Name { get; set; } = null!; // Đổi VehicleTypeName thành Name (VD: "Xe 4 chỗ", "Xe tải nhẹ", "Xe máy")
        public string? Description { get; set; } // Mô tả về loại xe
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
