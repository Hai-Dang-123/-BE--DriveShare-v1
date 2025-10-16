using Common.DTOs;
using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IEKYCService
    {
        Task<string?> UploadFileAsync(EKYCUploadRequestDTO requestDto);
        Task<ResponseDTO> OcrAsync(EKYCOcrRequestDTO dto);

        //HoangTest
        Task<string> ReadVehicleDocumentAsync(string frontUrl, string? backUrl);
    }
}
