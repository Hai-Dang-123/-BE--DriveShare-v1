using BLL.Services.Interface;
using Common.Constants;
using Common.DTOs;
using Common.Enums;
using Common.Messages;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> LoginAsync(LoginDTO dto)
        {
            // kiểm tra email
            var user = await _unitOfWork.UserRepo.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new ResponseDTO(AuthMessages.USER_NOT_FOUND, 404, false);
            }

            // kiểm tra mật khẩu
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return new ResponseDTO(AuthMessages.INVALID_CREDENTIALS, 400, false);
            }

            //kiểm tra token
            var exitsRefreshToken = await _unitOfWork.UserTokenRepo.GetRefreshTokenByUserID(user.UserId);
            if (exitsRefreshToken != null)
            {
                exitsRefreshToken.IsRevoked = true;
                await _unitOfWork.UserTokenRepo.UpdateAsync(exitsRefreshToken);
            }

            //khởi tạo claim
            var claims = new List<Claim>
            {
                new Claim(JwtConstant.KeyClaim.userId, user.UserId.ToString()),
                new Claim(JwtConstant.KeyClaim.Role, user.Role.RoleName)
            };

            //tạo refesh token
            var refreshTokenKey = JwtProvider.GenerateRefreshToken(claims);
            var accessTokenKey = JwtProvider.GenerateAccessToken(claims);

            var refreshToken = new UserToken
            {
                UserTokenId = Guid.NewGuid(),
                TokenValue = refreshTokenKey,
                UserId = user.UserId,
                IsRevoked = false,
                TokenType = TokenType.REFRESH,
                CreatedAt = DateTime.UtcNow
            };


            try
            {
                await _unitOfWork.UserTokenRepo.AddAsync(refreshToken);

                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO(AuthMessages.ERROR_OCCURRED, 500, false);
            }


            return new ResponseDTO(AuthMessages.LOGIN_SUCCESS, 200, true, new
            {
                AccessToken = accessTokenKey,
                RefreshToken = refreshToken.TokenValue,
            });


        }

        public async Task<ResponseDTO> RegisterAsync(RegisterDTO dto)
        {
            // kiểm tra email
            var user = await _unitOfWork.UserRepo.FindByEmailAsync(dto.Email);
            if (user != null)
            {
                return new ResponseDTO(AuthMessages.EMAIL_ALREADY_IN_USE, 400, false);
            }
            // kiểm tra số điện thoại
            var userByPhone = await _unitOfWork.UserRepo.FindByPhoneNumberAsync(dto.PhoneNumber);
            if (userByPhone != null)
            {
                return new ResponseDTO(AuthMessages.PHONE_ALREADY_IN_USE, 400, false);
            }

            // lấy role user
            var role = await _unitOfWork.RoleRepo.GetByName(dto.RoleName);
            if (role == null)
            {
                return new ResponseDTO(AuthMessages.ERROR_OCCURRED, 500, false);
            }

            // tạo user
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                UserName = dto.FullName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = role.RoleId,
                UserStatus = UserStatus.ACTIVE,
            };

            try
            {
                await _unitOfWork.UserRepo.AddAsync(newUser);
                await _unitOfWork.SaveChangeAsync();

                // gửi email xác thực
                var token = Guid.NewGuid().ToString();

                var emailToken = new UserToken
                {
                    UserTokenId = Guid.NewGuid(),
                    TokenValue = token,
                    UserId = newUser.UserId,
                    IsRevoked = false,
                    TokenType = TokenType.EMAIL_VERIFICATION,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.UserTokenRepo.AddAsync(emailToken);
                await _unitOfWork.SaveChangeAsync();

                // send email
                // TODO: gửi email xác thực
                // await _emailService.SendVerificationEmail(newUser.Email, token);

            }
            catch (Exception ex)
            {
                return new ResponseDTO(AuthMessages.ERROR_OCCURRED, 500, false);
            }
            return new ResponseDTO(AuthMessages.REGISTRATION_SUCCESS, 200, true);
        }

        public async Task<ResponseDTO> RefreshTokenAsync(string refreshToken)
        {
            var isValid = JwtProvider.Validation(refreshToken);
            if (!isValid)
            {
                return new ResponseDTO(AuthMessages.INVALID_REFRESH_TOKEN, 400, false);
            }
            var existingToken = await _unitOfWork.UserTokenRepo.GetByTokenValueAsync(refreshToken);
            if (existingToken == null || existingToken.IsRevoked || existingToken.TokenType != TokenType.REFRESH)
            {
                return new ResponseDTO(AuthMessages.INVALID_REFRESH_TOKEN, 400, false);
            }
            var user = await _unitOfWork.UserRepo.GetByIdAsync(existingToken.UserId);
            if (user == null)
            {
                return new ResponseDTO(AuthMessages.USER_NOT_FOUND, 404, false);
            }
            //khởi tạo claim
            var claims = new List<Claim>
            {
                new Claim(JwtConstant.KeyClaim.userId, user.UserId.ToString()),
                new Claim(JwtConstant.KeyClaim.Role, user.Role.RoleName)
            };
            //tạo refesh token
            var newRefreshTokenKey = JwtProvider.GenerateRefreshToken(claims);
            var newAccessTokenKey = JwtProvider.GenerateAccessToken(claims);
            // thu hồi refresh token cũ
            existingToken.IsRevoked = true;
            await _unitOfWork.UserTokenRepo.UpdateAsync(existingToken);
            // lưu refresh token mới
            var newRefreshToken = new UserToken
            {
                UserTokenId = Guid.NewGuid(),
                TokenValue = newRefreshTokenKey,
                UserId = user.UserId,
                IsRevoked = false,
                TokenType = TokenType.REFRESH,
                CreatedAt = DateTime.UtcNow
            };
            try
            {
                await _unitOfWork.UserTokenRepo.AddAsync(newRefreshToken);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO(AuthMessages.ERROR_OCCURRED, 500, false);
            }
            return new ResponseDTO("Refresh token success", 200, true, new
            {
                AccessToken = newAccessTokenKey,
                RefreshToken = newRefreshToken.TokenValue,
            });


        }

        public Task<ResponseDTO> VerifyEmailAsync(string token)
        {
            throw new NotImplementedException();
        }

        //public async Task<ResponseDTO> VerifyOTPAsync(VerifyOTPPhoneNumberDTO dto)
        //{
            
        //}

        public Task<ResponseDTO> LogoutAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> ForgotPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            var user = await _unitOfWork.UserRepo.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new ResponseDTO(AuthMessages.USER_NOT_FOUND, 404, false);
            }
            var userToken = await _unitOfWork.UserTokenRepo.GetByTokenValueAsync(dto.Token);
            if (userToken == null || userToken.IsRevoked || userToken.TokenType != TokenType.RESET_PASSWORD || userToken.UserId != user.UserId)
            {
                return new ResponseDTO(AuthMessages.INVALID_TOKEN, 400, false);
            }

            // gửi userId để xác thực + token qua email
            // send email

            return new ResponseDTO("Send email already", 200, true);

        }

        //public async Task<ResponseDTO> ChangePasswordAsync(ChangePasswordDTO dto)
        //{
        //    var user = await _unitOfWork.UserRepo.FindByEmailAsync(dto.Email);
        //    if (user == null)
        //    {
        //        return new ResponseDTO(AuthMessages.USER_NOT_FOUND, 404, false);
        //    }
        //    // kiểm tra mật khẩu cũ
        //    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.PasswordHash);
        //    if (!isPasswordValid)
        //    {
        //        return new ResponseDTO(AuthMessages.INVALID_CREDENTIALS, 400, false);
        //    }
        //    // cập nhật mật khẩu mới
        //    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        //    try
        //    {
        //        await _unitOfWork.UserRepo.UpdateAsync(user);
        //        await _unitOfWork.SaveChangeAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDTO(AuthMessages.ERROR_OCCURRED, 500, false);
        //    }
        //    return new ResponseDTO("Change password success", 200, true);

        //}


        
    }
}
