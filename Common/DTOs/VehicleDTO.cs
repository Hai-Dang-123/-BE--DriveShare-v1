using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class CreateVehicleDTO
    {
        [Required(ErrorMessage = "Plate number is required")]
        [StringLength(20, ErrorMessage = "Plate number cannot exceed 20 characters")]
        public string PlateNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Brand is required")]
        [StringLength(50, ErrorMessage = "Brand cannot exceed 50 characters")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "VehicleTypeId is required")]
        public Guid VehicleTypeId { get; set; }
    }
    public class UpdateVehicleDTO
    {
        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        [StringLength(20)]
        public string PlateNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public Guid VehicleTypeId { get; set; }
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
    }

}
