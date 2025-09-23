using BLL.Services.Interface;
using Common.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }


        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, false);
            await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        public async Task SendEmailRegisterSuccessAsync(string fullName, string email, string token)
        {
            string subject = "🐾 Chào mừng bạn đến với PACP - Nền tảng cứu hộ động vật!";

            string activationLink = $"https://pacp-fe-lai-vu-hai-dang-se151369s-projects.vercel.app/active-account?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

            string body = $@"
<!DOCTYPE html>
<html lang='vi'>
<head>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            color: #333;
            padding: 20px;
        }}
        .container {{
            max-width: 600px;
            margin: auto;
            background: #fff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
        }}
        h2 {{
            color: #28a745;
        }}
        p {{
            line-height: 1.6;
        }}
        .btn {{
            display: inline-block;
            padding: 10px 20px;
            background-color: #28a745;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            margin-top: 20px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Chào mừng, {fullName}!</h2>
        <p>Cảm ơn bạn đã đăng ký tài khoản tại <strong>PACP - Nền tảng cứu hộ động vật</strong>.</p>
        <p>Chúng tôi rất vui khi bạn trở thành một phần của cộng đồng yêu thương và bảo vệ động vật.</p>
        <p>Để tiếp tục, hãy kích hoạt tài khoản của bạn bằng cách nhấn vào nút bên dưới:</p>
        <p><a href='{activationLink}' class='btn'>Kích hoạt tài khoản</a></p>
        <p>Nếu nút không hoạt động, bạn có thể sao chép và dán liên kết này vào trình duyệt:</p>
        <p><a href='{activationLink}'>{activationLink}</a></p>
        <p>Mọi thắc mắc, vui lòng liên hệ chúng tôi qua email <a href='mailto:support@pacp.example.com'>support@pacp.example.com</a>.</p>
        <p>❤️ Cùng nhau lan tỏa yêu thương, cứu lấy những sinh linh nhỏ bé!<br>— Đội ngũ PACP</p>
    </div>
</body>
</html>";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendEmailForgotPasswordAsync(string fullName, string email, string token)
        {
            string subject = "🔐 Yêu cầu đặt lại mật khẩu - PACP";

            string resetLink = $"https://pacp-fe-lai-vu-hai-dang-se151369s-projects.vercel.app/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

            string body = $@"
<!DOCTYPE html>
<html lang='vi'>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            color: #333;
            padding: 20px;
        }}
        .container {{
            max-width: 600px;
            margin: auto;
            background: #fff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
        }}
        h2 {{
            color: #dc3545;
        }}
        .btn {{
            display: inline-block;
            padding: 10px 20px;
            background-color: #dc3545;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            margin-top: 20px;
        }}
        p {{
            line-height: 1.6;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Xin chào, {fullName}!</h2>
        <p>Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn tại <strong>PACP</strong>.</p>
        <p>Nếu bạn là người gửi yêu cầu, vui lòng nhấn vào nút bên dưới để tiến hành khôi phục mật khẩu:</p>
        <p><a href='{resetLink}' class='btn'>Đặt lại mật khẩu</a></p>
        <p>Nếu nút không hoạt động, hãy sao chép và dán liên kết sau vào trình duyệt:</p>
        <p><a href='{resetLink}'>{resetLink}</a></p>
        <p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p>
        <p>— Đội ngũ PACP</p>
    </div>
</body>
</html>";

            await SendEmailAsync(email, subject, body);
        }
    }
}
