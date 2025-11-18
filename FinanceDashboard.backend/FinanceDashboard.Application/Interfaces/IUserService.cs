using FinanceDashboard.Application.DTOs.User;
using FinanceDashboard.Application.DTOs.User.Result;
using System.Security.Claims;

namespace FinanceDashboard.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetAsync(string userId);
        Task<RegisterResultDTO> RegisterAsync(RegisterDTO registerDto);
        Task<LoginResultDTO> LoginAsync(LoginDTO loginDto);
        Task<UserProfileDTO> GetProfileAsync(string userId);
    }
}
