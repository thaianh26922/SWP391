using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SWP391.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace SWP391.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Models.User user = new Models.User();
        private readonly motelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public AuthController(
           motelContext context,
           IWebHostEnvironment hostEnvironment,
           IConfiguration configuration

           )
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;

        }
        [HttpPost("register")]
        public ActionResult<Models.User> Register(Models.UserDto request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.password);
            var newUser = new Models.User
            {
                Username = request.userName,
                Password = passwordHash,
                Email = request.email,
                Role = "User"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok(newUser);
        }
        [HttpPost("login")]
        public  ActionResult<Models.User> Login(UserDto request)
        {
            var UserLogin =  _context.Users.FirstOrDefault(p => p.Username == request.userName);

            if (UserLogin == null)
            {
                return NotFound("User not found.");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.password, UserLogin.Password))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(UserLogin);
            /*var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);*/

            return Ok(token);
        }
        /*[HttpPost("refresh-token")]

        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);
            return Ok(token);
        }*/
      /*  private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expired = DateTime.Now.AddDays(7)
            };
            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expired
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpired = newRefreshToken.Expired;
        }*/
        private string CreateToken(Models.User user)
        {
            List<Claim> claims = new List<Claim> {
                       new Claim(ClaimTypes.Name, user.Username),
                       new Claim(ClaimTypes.Role, "Admin"),
                       new Claim(ClaimTypes.Role, "User"),
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
