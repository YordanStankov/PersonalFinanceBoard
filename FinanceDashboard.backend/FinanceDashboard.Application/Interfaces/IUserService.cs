using FinanceDashboard.Application.DTOs.User;

namespace FinanceDashboard.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(string userId);
    }
}
