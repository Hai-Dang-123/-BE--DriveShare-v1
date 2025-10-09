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
    }

    public class ChangeStatusPostVehicleDTO
    {
        [Required(ErrorMessage = "PostVehicleId is required")]
        public Guid PostVehicleId { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public PostStatus Status { get; set; }
    }

}
