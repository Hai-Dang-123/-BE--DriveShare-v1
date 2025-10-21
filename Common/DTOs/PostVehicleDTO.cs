using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "ClauseId is required")]
        public Guid ClauseId { get; set; }
        [Required(ErrorMessage = "VehicleId is required")]
        public Guid VehicleId { get; set; }

        [Required(ErrorMessage = "DailyPrice is required")]
        [Range(1, double.MaxValue, ErrorMessage = "DailyPrice must be greater than 0")]
        public decimal DailyPrice { get; set; }

        [Required(ErrorMessage = "StartDate is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required")]
        public DateTime EndDate { get; set; }


        //public List<AddOptionDTO>? AddOptions { get; set; }



    }
    public class UpdateRequestPostVehicleDTO {
        [Required(ErrorMessage = "ClauseId is required")]
        public Guid ClauseId { get; set; }
        public Guid PostVehicleId { get; set; }
        [Required(ErrorMessage = "DailyPrice is required")]
        [Range(1, double.MaxValue, ErrorMessage = "DailyPrice must be greater than 0")]
        public decimal DailyPrice { get; set; }
        [Required(ErrorMessage = "StartDate is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "EndDate is required")]
        public DateTime EndDate { get; set; }

    }
    public class GetPostVehicleDTO
    {
        public Guid ClauseId { get; set; }
        public string OwnerPhone{ get; set; }
        public string OwnerName{ get; set; }
        public string VehicleModel { get; set; }
        public string VehicleBrand { get; set; }
        public string PlateNumber { get; set; }
        public string VehicleType { get; set; } 
        public decimal DailyPrice { get; set; }
        public PostStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> ImageUrls { get; set; } = new();

    }

    public class ChangeStatusPostVehicleDTO
    {
        [Required(ErrorMessage = "PostVehicleId is required")]
        public Guid PostVehicleId { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public PostStatus Status { get; set; }
    }

    public class PostVehicleResponseDTO
    {
        public Guid PostVehicleId { get; set; }
        public decimal DailyPrice { get; set; }
        public string? Description { get; set; }
        public PostStatus Status { get; set; }
        public DateTime AvailableStartDate { get; set; }
        public DateTime AvailableEndDate { get; set; }

        // ====== Quan hệ ======
        public VehicleSummaryDTO Vehicle { get; set; } = null!;
        public OwnerSummaryDTO Owner { get; set; } = null!;
        public ClauseTemplateResponseDTO ClauseTemplate { get; set; } = null!;
        public List<AddOptionResponseDTO> AddOptions { get; set; } = new();
    }

    // ====== Tóm tắt xe (Vehicle) ======
    public class VehicleSummaryDTO
    {
        public Guid VehicleId { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string VehicleType { get; set; } = null!;
        public string PlateNumber { get; set; } = null!;
        public List<string> ImageUrls { get; set; } = new();
    }

    // ====== Chủ xe (Owner) ======
    public class OwnerSummaryDTO
    {
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; } = null!;
        public string OwnerPhone { get; set; } = null!;
    }

    // ====== Mẫu điều khoản (Clause Template) ======
    public class ClauseTemplateResponseDTO
    {
        public Guid ClauseId { get; set; }
        public string Version { get; set; } = null!;
        public string Title { get; set; } = null!;
        public ClauseTemplateStatus Status { get; set; }

        public List<ClauseContentResponseDTO> ClauseContents { get; set; } = new();
    }


    // ====== Tùy chọn thêm của bài đăng (AddOptions) ======
    public class AddOptionResponseDTO
    {
        public Guid AddOptionId { get; set; }
        public string Description { get; set; } = null!;
    }


}
