using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Messages
{
    public class PostMessages
    {
        
        public const string POST_CREATED_SUCCESS = "Post created successfully.";
        public const string POST_NOT_FOUND = "Post not found.";
        public const string ERROR_OCCURRED = "An error occurred. Please try again.";
        public const string POST_UPDATED_SUCCESS = "Post updated successfully.";
        public const string POST_DELETED_SUCCESS = "Post deleted successfully.";
        public const string FORBIDDEN = "You do not have permission to perform this action.";
        public const string GET_ALL_POST_SUCCESS = "Get all posts successfully.";
        public const string GET_POST_SUCCESS = "Get post successfully.";
        public const string POST_RENTED = "Cannot delete because post is already RENTED or DELETED";
    }
}
