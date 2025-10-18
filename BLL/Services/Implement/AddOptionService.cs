using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    using BLL.Services.Interface;
    using Common.DTOs;
    using Common.Messages;
    using DAL.Entities;
    using DAL.UnitOfWork;
    using global::BLL.Services.Interface;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    namespace BLL.Services.Implement
    {
        public class AddOptionService : IAddOptionService
        {
            private readonly IUnitOfWork _unitOfWork;

            public AddOptionService(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ResponseDTO> CreateAddOptionAsync(CreateAddOptionDTO dto)
            {
                // Kiểm tra PostVehicleId có tồn tại không
                var postVehicle = await _unitOfWork.PostVehicleRepo.GetPostByIdAsync(dto.PostVehicleId);
                if (postVehicle == null)
                {
                    return new ResponseDTO("PostVehicle không tồn tại.", 404, false);
                }

                // Tạo mới AddOption
                var newAddOption = new AddOption
                {
                    AddOptionId = Guid.NewGuid(),
                    Description = dto.Description,
                    PostVehicleId = dto.PostVehicleId
                };

                try
                {
                    await _unitOfWork.AddOptionRepo.AddAsync(newAddOption);
                    await _unitOfWork.SaveChangeAsync();
                }
                catch (Exception ex)
                {
                    // Log lỗi nếu cần
                    return new ResponseDTO("Đã xảy ra lỗi khi tạo AddOption.", 500, false);
                }

                return new ResponseDTO("Tạo AddOption thành công.", 201, true);
            }

            public async Task<ResponseDTO> GetAllAddOptionsAsync()
            {
                var addOptions = await _unitOfWork.AddOptionRepo.GetAllAsync();
                if (!addOptions.Any())
                {
                    return new ResponseDTO("Không có AddOption nào.", 404, false);
                }

                return new ResponseDTO("Lấy danh sách AddOption thành công.", 200, true, addOptions);
            }

            public async Task<ResponseDTO> GetAddOptionByIdAsync(Guid id)
            {
                var addOption = await _unitOfWork.AddOptionRepo.GetByIdAsync(id);
                if (addOption == null)
                {
                    return new ResponseDTO("AddOption không tồn tại.", 404, false);
                }

                return new ResponseDTO("Lấy AddOption thành công.", 200, true, addOption);
            }

            public async Task<ResponseDTO> UpdateAddOptionAsync(Guid id, CreateAddOptionDTO dto)
            {
                var addOption = await _unitOfWork.AddOptionRepo.GetByIdAsync(id);
                if (addOption == null)
                {
                    return new ResponseDTO("AddOption không tồn tại.", 404, false);
                }

                // Cập nhật thông tin
                addOption.Description = dto.Description;
                addOption.PostVehicleId = dto.PostVehicleId;

                try
                {
                    await _unitOfWork.AddOptionRepo.UpdateAsync(addOption);
                    await _unitOfWork.SaveChangeAsync();
                }
                catch (Exception ex)
                {
                    // Log lỗi nếu cần
                    return new ResponseDTO("Đã xảy ra lỗi khi cập nhật AddOption.", 500, false);
                }

                return new ResponseDTO("Cập nhật AddOption thành công.", 200, true);
            }

            public async Task<ResponseDTO> DeleteAddOptionAsync(Guid id)
            {
                var addOption = await _unitOfWork.AddOptionRepo.GetByIdAsync(id);
                if (addOption == null)
                {
                    return new ResponseDTO("AddOption không tồn tại.", 404, false);
                }

                try
                {
                    await _unitOfWork.AddOptionRepo.DeleteAsync(addOption);
                    await _unitOfWork.SaveChangeAsync();
                }
                catch (Exception ex)
                {
                    // Log lỗi nếu cần
                    return new ResponseDTO("Đã xảy ra lỗi khi xóa AddOption.", 500, false);
                }

                return new ResponseDTO("Xóa AddOption thành công.", 200, true);
            }
        }
    }
}