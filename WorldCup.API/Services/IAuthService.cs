using WorldCup.API.DTOs.Auth;

namespace WorldCup.API.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto request);
        Task<string> LoginAsync(LoginDto request);
    }
}
