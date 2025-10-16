using Common.DTOs;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IVerificationService
    {
        Task<ResponseDTO> UploadDocumentsAsync(UploadVerificationDTO dto);
        Task<ResponseDTO> SendOcrRequestAsync(Guid verificationId);
        Task<ResponseDTO> CreateVerificationAsync(CreateVerificationDTO dto);
        Task<ResponseDTO> GetVerificationByVehicleIdAsync(Guid vehicleId);
        Task<ResponseDTO> GetMyVerificationStatusAsync();
    }
}
