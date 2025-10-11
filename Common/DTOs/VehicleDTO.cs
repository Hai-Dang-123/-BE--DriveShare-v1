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

        [Required]
        public Guid VehicleTypeId { get; set; }

        // Upload ảnh kèm theo
        public int year { get; set; }
        public string color { get; set; }  
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
        public Guid VehicleTypeId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; } = string.Empty;

        // Danh sách URL ảnh

        public List<string> ImageUrls { get; set; } = new();
    }
    public class ChangeVehicleStatusDTO
    {
        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public VehicleStatus NewStatus { get; set; }
    }

}

