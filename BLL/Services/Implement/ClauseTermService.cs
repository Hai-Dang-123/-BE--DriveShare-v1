using BLL.Services.Interface;
using Common.DTOs;
using DAL.UnitOfWork;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ClauseTermService : IClauseTermService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ClauseTermService> _logger;

        public ClauseTermService(IUnitOfWork unitOfWork, ILogger<ClauseTermService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ResponseDTO> DeleteClauseTermAsync(Guid id)
        {
            try
            {
                var clauseTerm = await _unitOfWork.ClauseTermRepo.GetByIdAsync(id);
                if (clauseTerm == null)
                {
                    return new ResponseDTO
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Clause term not found."
                    };
                }

                _unitOfWork.ClauseTermRepo.Delete(clauseTerm);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Clause term deleted successfully."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting clause term");
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Error deleting clause term: {ex.Message}"
                };
            }
        }
    }
}
