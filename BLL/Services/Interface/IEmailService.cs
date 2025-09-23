using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IEmailService
    {
        Task SendEmailRegisterSuccessAsync(string fullName, string email, string token);
        Task SendEmailForgotPasswordAsync(string fullName, string email, string token);
    }
}
