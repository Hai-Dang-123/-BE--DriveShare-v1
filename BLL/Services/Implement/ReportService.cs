using BLL.Services.Interface;
using Common.DTOs;
using DAL.UnitOfWork;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReportService> _logger;

        public ReportService(IUnitOfWork unitOfWork, ILogger<ReportService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ResponseDTO> DeleteReportAsync(Guid id)
        {
            try
            {
                var report = await _unitOfWork.ReportRepo.GetByIdAsync(id);
                if (report == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Report not found."
                    };
                }

                _unitOfWork.ReportRepo.Delete(report);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Report deleted successfully."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting report");
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Error deleting report: {ex.Message}"
                };
            }
        }
    }
}
