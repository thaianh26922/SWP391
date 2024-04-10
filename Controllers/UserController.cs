using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using SWP391.Account;
using SWP391.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SWP391.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly SmtpClient _smtpClient;
        private readonly motelContext _context;

        public UserController(motelContext context)
        {
            // Khởi tạo đối tượng SmtpClient
            _smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false, // Tắt sử dụng thông tin đăng nhập mặc định
                Credentials = new NetworkCredential("anhpthe161502@fpt.edu.vn", "lncu wezr xpah qzdu"), // Đăng nhập vào SMTP server
                EnableSsl = true // Kích hoạt SSL/TLS
            };
            _context = context;


        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string email , string code)
        {
            try
            {
                // Tạo đối tượng MailMessage
                string body = @"
    <html>
    <head>
        <style>
            body {
                font-family: Arial, sans-serif;
            }
            h1 {
                color: #333333;
            }
            p {
                color: #666666;
            }
        </style>
    </head>
    <body>
        <h1>Hello,</h1>
        <p>This is a sample email body. You can customize it as needed.</p>
        <p>Best regards,<br/>Your Name</p>
    </body>
    </html>
";
                MailMessage message = new MailMessage
                {
                    From = new MailAddress("anhpthe161502@fpt.edu.vn"), // Địa chỉ email người gửi
                    Subject = "this is tittle", // Tiêu đề email
                    Body = body // Nội dung email
                };
                message.To.Add(email); // Địa chỉ email người nhận

                // Gửi email
                await _smtpClient.SendMailAsync(message);

                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send email: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getUser")]
        [Authorize]

        public  IActionResult TakeUser()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpPut("changePassword")]
        public ActionResult<Models.User> ChangePassword(ChangePassword changePassword)
        {
            var UserLogin = _context.Users.FirstOrDefault(p => p.Username == changePassword.userName);

            if (UserLogin == null)
            {
                return NotFound("User not found.");
            }
            if (!BCrypt.Net.BCrypt.Verify(changePassword.OldPassword, UserLogin.Password))
            {
                return BadRequest("Wrong password.");
            }
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword);
            UserLogin.Password = passwordHash;
            /*var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);*/
            _context.Users.Update(UserLogin);
            _context.SaveChanges()
            return Ok(UserLogin);
        }
    }
    
    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}