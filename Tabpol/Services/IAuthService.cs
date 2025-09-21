using Tabpol.Entities;
using Tabpol.Models;
using Tabpol.Models.Requests;
using Tabpol.Models.Responses;

namespace Tabpol.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
        Task<UserResponseDto?> GetUserDataAsync(UserDataRequestDto request);
    }
}
