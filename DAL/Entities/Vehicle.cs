using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; }
        public string PlateNumber { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public int YearOfManufacture { get; set; } // Thêm năm sản xuất
        public string Color { get; set; } = null!;

        public Guid VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; } = null!;

        // Mỗi xe có thể có nhiều xác minh qua thời gian (đăng kiểm, sở hữu...)
        // Hoặc chỉ một xác minh đang Active
        public Guid? CurrentVerificationId { get; set; } // Xác minh gần nhất/hiện tại
        public Verification? CurrentVerification { get; set; }

        public Guid OwnerUserId { get; set; } // Đổi UserId thành OwnerUserId
        public User OwnerUser { get; set; } = null!;

        public VehicleStatus Status { get; set; } = VehicleStatus.ACTIVE;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<PostVehicle> PostsForRent { get; set; } = new List<PostVehicle>(); // Đổi Posts
        public ICollection<VehicleImage> Images { get; set; } = new List<VehicleImage>(); // Đổi VehicleImages
        public ICollection<VehicleInspection> Inspections { get; set; } = new List<VehicleInspection>(); // Thêm nếu muốn xem lịch sử kiểm tra xe
        public ICollection<Review> Reviews { get; set; } = new List<Review>(); // Đánh giá về xe này
        public ICollection<PostItem> PostItems { get; set; } = new List<PostItem>();
    }

}
