using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTOs;
using Common.Enums;
using Common.Messages;
using DAL.Entities;
using DAL.UnitOfWork;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        public UserService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }

        public async Task<ResponseDTO> CreateUserAsync(CreateUserDTO dto)
        {
            // check user đã có trong hệ thống hay chưa
            var checkduplicate = await _unitOfWork.UserRepo.FindByEmailAsync(dto.Email);
            if (checkduplicate != null)
            {
                return new ResponseDTO(UserMessages.DUPLICATED_USER, 400, false);
            }

            // getRole từ roleName
            var role = await _unitOfWork.RoleRepo.GetByName(dto.Role);
            if (role == null)
            {
                return new ResponseDTO(UserMessages.ROLE_NOT_FOUND, 404, false);
            }

            // hash password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // tạo mới user
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = hashedPassword,
                UserName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                RoleId = role.RoleId,
                UserStatus = UserStatus.ACTIVE
            };

            // lưu vào db ( nhớ try - catch )
            try 
            { 
                await _unitOfWork.UserRepo.AddAsync(newUser);
                await _unitOfWork.SaveChangeAsync();


                
            }
            catch (Exception ex)
            {
                // log ex
                return new ResponseDTO(UserMessages.ERROR_OCCURRED, 500, false);
            }

            return new ResponseDTO(UserMessages.USER_CREATED_SUCCESS, 201, true);
        }

        public async Task<ResponseDTO> DeleteUserAsync ()
        {
            // lấy userId từ token
            var userId = _userUtility.GetUserIdFromToken();
            if ( userId == Guid.Empty )
            {
                return new ResponseDTO(UserMessages.UNAUTHORIZED, 400, false);
            }

            // tìm user trong db
            var user = await _unitOfWork.UserRepo.GetByIdAsync(userId);
            if ( user == null )
            {
                return new ResponseDTO(UserMessages.USER_NOT_FOUND, 404, false);
            }

            // xóa user ( soft )
            user.UserStatus = UserStatus.DELETED;

            // lưu lại vào data
            try
            {
                await _unitOfWork.UserRepo.UpdateAsync(user);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                // log ex
                return new ResponseDTO(UserMessages.ERROR_OCCURRED, 500, false);
            }

            return new ResponseDTO(UserMessages.USER_DELETED_SUCCESS, 200, true);

        }
    }
}
