using BLL.Services.Interface;
using Common.DTOs;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ContractTermService : IContractTermService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContractTermService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ✅ Lấy tất cả điều khoản
        public async Task<ResponseDTO> GetAllContractTermsAsync()
        {
            try
            {
                var terms = await _unitOfWork.ContractTermRepo
                    .GetAll() // ✅ dùng GetAll() thay vì AsQueryable()
                    .Include(t => t.ContractTemplate)
                    .ToListAsync();

                if (terms == null || !terms.Any())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Không có điều khoản hợp đồng nào."
                    };
                }

                var result = terms.Select(t => new
                {
                    t.ContractTermId,
                    t.Content,
                    t.IsMandatory,
                    t.ContractTemplateId,
                    ContractTemplateName = t.ContractTemplate?.Name
                });

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Lấy danh sách điều khoản hợp đồng thành công.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = $"Lỗi khi lấy điều khoản hợp đồng: {ex.Message}"
                };
            }
        }


        // ✅ Lấy điều khoản theo ID (bạn đã có)
        public async Task<ResponseDTO> GetContractTermByIdAsync(Guid id)
        {
            try
            {
                var term = await _unitOfWork.ContractTermRepo
                     .GetAll()
                     .Include(t => t.ContractTemplate)
                     .FirstOrDefaultAsync(t => t.ContractTermId == id);


                if (term == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Không tìm thấy điều khoản hợp đồng."
                    };
                }

                var result = new
                {
                    term.ContractTermId,
                    term.Content,
                    term.IsMandatory,
                    term.ContractTemplateId,
                    ContractTemplateName = term.ContractTemplate?.Name
                };

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Lấy điều khoản hợp đồng thành công.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = $"Lỗi khi lấy điều khoản hợp đồng: {ex.Message}"
                };
            }
        }
    }
}
