using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class EKYCUploadRequestDTO
    {
        public IFormFile File { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
