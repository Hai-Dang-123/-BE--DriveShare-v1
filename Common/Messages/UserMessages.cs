using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Messages
{
    public static class UserMessages
    {
        public const string DUPLICATED_USER = "User is already created.";
        public const string USER_NOT_FOUND = "User not found.";
        public const string INVALID_PASSWORD = "Password is invalid.";
        public const string USER_LOCKED = "User account is locked.";
        public const string ROLE_NOT_FOUND = "Role not found.";
        public const string UNAUTHORIZED = "Unauthorized access.";

        public const string USER_CREATED_SUCCESS = "User created successfully.";
        public const string USER_UPDATED_SUCCESS = "User updated successfully.";
        public const string USER_DELETED_SUCCESS = "User deleted successfully.";

        public const string ERROR_OCCURRED = "An error occurred. Please try again.";
    }
}
