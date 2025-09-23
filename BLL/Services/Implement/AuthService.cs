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
    }
}
