using BLL.Services.Interface;
using Common.DTOs;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ReportTermService : IReportTermService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReportTermService> _logger;

        public ReportTermService(IUnitOfWork unitOfWork, ILogger<ReportTermService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        
        

        public async Task<ResponseDTO> GetAllReportTermsAsync()
        {
            try
            {
                var terms = await _unitOfWork.ReportTermRepo.GetAll().ToListAsync();

                if (terms == null || !terms.Any())
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Không có điều khoản báo cáo nào."
                    };
                }

                var result = terms.Select(t => new
                {
                    t.ReportTermId,
                    t.Content,
                    t.IsMandatory,
                    t.ReportTemplateId
                });

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Lấy danh sách điều khoản báo cáo thành công.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Lỗi khi lấy điều khoản báo cáo: {ex.Message}"
                };
            }
        }

        // ✅ Lấy điều khoản báo cáo theo ID
        public async Task<ResponseDTO> GetReportTermByIdAsync(Guid id)
        {
            try
            {
                var term = await _unitOfWork.ReportTermRepo.GetByIdAsync(id);

                if (term == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Không tìm thấy điều khoản báo cáo."
                    };
                }

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Lấy điều khoản báo cáo thành công.",
                    Result = term
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Lỗi khi lấy điều khoản báo cáo: {ex.Message}"
                };
            }
        }
        public async Task<ResponseDTO> DeleteReportTermAsync(Guid id)
        {
            try
            {
                var term = await _unitOfWork.ReportTermRepo.GetByIdAsync(id);
                if (term == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Report term not found."
                    };
                }

                _unitOfWork.ReportTermRepo.Delete(term);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Report term deleted successfully."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting report term");
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Error deleting report term: {ex.Message}"
                };
            }
        }
    }
}
