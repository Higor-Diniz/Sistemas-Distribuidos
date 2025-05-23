using BlogAPI.DTOs.Auth;

namespace BlogAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> RegisterAsync(UserRegistrationDto registrationDto);
        Task<ServiceResponse<string>> LoginAsync(UserLoginDto loginDto);
    }
} 