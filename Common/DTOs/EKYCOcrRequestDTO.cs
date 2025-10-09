using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class EKYCOcrRequestDTO
    {
        public string ImgFront { get; set; }
        public string ImgBack { get; set; }
        public string ClientSession { get; set; }
        public int? Type { get; set; } = -1;
        public bool? ValidatePostcode { get; set; } = false;
        public string CropParam { get; set; }
        public string Token { get; set; }
        public string? ChallengeCode { get; set; } = "1111";
        public string? MacAddress { get; set; } = "TEST1";
    }
}
