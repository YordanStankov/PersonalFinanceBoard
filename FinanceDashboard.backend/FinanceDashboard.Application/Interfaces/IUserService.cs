using FinanceDashboard.Application.DTOs.User;
using FinanceDashboard.Application.DTOs.User.Result;
using System.Security.Claims;

namespace FinanceDashboard.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(string userId);
        Task<RegisterResultDTO> RegisterUserAsync(RegisterDTO registerDto);
        Task<LoginResultDTO> LoginUser(LoginDTO loginDto);
        Task<UserProfileDTO> GetUserProfileAsync(string userId);
    }
}
