using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Messages
{
    public static class AuthMessages
    {
        public const string INVALID_CREDENTIALS = "Invalid username or password.";
        public const string USER_NOT_FOUND = "User not found.";
        public const string ACCOUNT_LOCKED = "Account is locked. Please contact support.";
        public const string TOKEN_EXPIRED = "Authentication token has expired.";
        public const string UNAUTHORIZED_ACCESS = "You do not have permission to access this resource.";
        public const string REGISTRATION_SUCCESS = "Registration successful. Please check your email to verify your account.";
        public const string EMAIL_ALREADY_IN_USE = "This email is already registered.";
        public const string PASSWORD_RESET_SUCCESS = "Password reset successful. You can now log in with your new password.";
        public const string INVALID_TOKEN = "The provided token is invalid or has expired.";
        public const string LOGIN_SUCCESS = "Login successful.";

        public const string ERROR_OCCURRED = "An error occurred. Please try again later.";
    }

}
