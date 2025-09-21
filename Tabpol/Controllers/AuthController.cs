using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tabpol.Entities;
using Tabpol.Models;
using Tabpol.Models.Requests;
using Tabpol.Models.Responses;
using Tabpol.Services;

namespace Tabpol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IConfiguration configuration) : ControllerBase
    {
        public static User user = new();
        public static PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest("Username already exists.");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var token = await authService.LoginAsync(request);
            if (token is null)
            {
                return BadRequest("Invalid username or password.");
            }
            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null || result.AccesToken is null || result.RefreshToken is null )
            {
                return BadRequest("Invalid or expired refresh token.");
            }

            return Ok(result);
        }

        [HttpGet("user-data")]
        public async Task<ActionResult<UserDto>> GetUserData([FromQuery]UserDataRequestDto request) 
        {
            var user = await authService.GetUserDataAsync(request);
            if (user is null) 
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("Admin authenticated");
        }
    }
}
