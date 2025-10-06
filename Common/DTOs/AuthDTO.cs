using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class AuthDTO
    {
    }

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
    }

    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class VerifyOTPPhoneNumberDTO
    {
        public string IdToken { get; set; }
    }

    public class ChangePasswordDTO
    {
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
