using FinanceDashboard.Domain.Models;
using System.Security.Claims;
namespace FinanceDashboard.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string userId);
        Task<string> RegisterUser(string userName, string email, string password);
        Task<User> LoginUser(string email, string password);
        Task<bool> CheckUserExistenceAsync(string userId);
    }
}
