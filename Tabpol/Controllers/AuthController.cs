using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tabpol.Entities;
using Tabpol.Models;

namespace Tabpol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        public static User user = new();
        public static PasswordHasher<User> passwordHasher = new PasswordHasher<User>(); 
        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            var hashedPassword = passwordHasher
                .HashPassword(user, request.Password);

            user.Username = request.Username;
            user.PasswordHash = hashedPassword;

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserDto request) 
        {
            if (user.Username != request.Username) 
            {
                return BadRequest("User not found");
            }
            var verifyHashedPasswordResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (verifyHashedPasswordResult == PasswordVerificationResult.Failed)
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
