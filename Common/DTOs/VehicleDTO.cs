using Common.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class CreateVehicleDTO
    {
        [Required(ErrorMessage = "Plate number is required")]
        [StringLength(20)]
        public string PlateNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required")]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Brand is required")]
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;

        // ✅ Thêm Color
        [Required(ErrorMessage = "Color is required")]
        [StringLength(30)]
        public string Color { get; set; } = string.Empty;

        [Required]
        public Guid VehicleTypeId { get; set; }

        // Upload ảnh kèm theo

        public int Year { get; set; }
        public List<IFormFile> Files { get; set; }

    }

    public class UpdateVehicleDTO
    {
        [Required]
        public Guid VehicleId { get; set; }

        [Required, StringLength(20)]
        public string PlateNumber { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Brand { get; set; } = string.Empty;

        // ✅ Thêm Color
        [Required, StringLength(30)]
        public string Color { get; set; } = string.Empty;

        [Required]
        public Guid VehicleTypeId { get; set; }

        // Thêm ảnh mới
        public List<IFormFile>? NewFiles { get; set; }

        // Danh sách ảnh cần xoá
        public List<Guid>? DeletedImageIds { get; set; }
    }

    public class VehicleReadDTO
    {
        public Guid VehicleId { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;

        // ✅ Thêm Color
        public string Color { get; set; } = string.Empty;

        public Guid VehicleTypeId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; } = string.Empty;

        // Danh sách URL ảnh
        public List<string> ImageUrls { get; set; } = new();
    }

    // 🔹 DETAIL DTOs
    // =============================

    public class VehicleImageDTO
    {
        public Guid ImageId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class OwnerDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class VehicleBasicDTO
    {
        public Guid VehicleId { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string VehicleTypeName { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string? Transmission { get; set; }
        public string? FuelType { get; set; }
        public int? Seats { get; set; }
        public List<VehicleImageDTO> Images { get; set; } = new();
    }

    public class VehicleDetailDTO
    {
        public Guid PostVehicleId { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public VehicleBasicDTO Vehicle { get; set; } = new();
        public OwnerDTO Owner { get; set; } = new();
    }

    public class ChangeVehicleStatusDTO
    {
        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public VehicleStatus NewStatus { get; set; }
    }
}

