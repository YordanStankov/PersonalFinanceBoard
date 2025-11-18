using FinanceDashboard.Domain.Models;
using System.Security.Claims;
namespace FinanceDashboard.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string userId);
        Task<string> RegisterAsync(string userName, string email, string password);
        Task<User> LoginAsync(string email, string password);
        Task<bool> CheckExistenceAsync(string userId);
    }
}
