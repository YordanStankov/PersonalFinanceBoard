using FinanceDashboard.Application.DTOs.User;
using System.Security.Claims;

namespace FinanceDashboard.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(string userId);
        Task<RegisterResultDTO> RegisterUser(RegisterDTO registerDto);
        Task<LoginResultDTO> LoginUser(LoginDTO loginDto);
        Task<UserProfileDTO> GetUserProfileAsync(ClaimsPrincipal userClaim);
    }
}
