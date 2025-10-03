using FinanceDashboard.Application.DTOs.User;

namespace FinanceDashboard.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(string userId);
        Task<RegisterResultDTO> RegisterUser(RegisterDTO registerDto);
        Task<LoginResultDTO> LoginUser(LoginDTO loginDto);
    }
}
