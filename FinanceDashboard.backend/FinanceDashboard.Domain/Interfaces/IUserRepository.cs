using FinanceDashboard.Domain.Models;
using System.Security.Claims;
namespace FinanceDashboard.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string userId);
        Task SaveChangesAsync();
        Task<User> GetByEmailAsync(string email);
        Task<bool> CheckExistenceAsync(string userId);
        Task<bool> CheckForExistingUserNameAsync(string userName);
        Task<bool> CheckForExistingEmailAsync(string email);
    }
}
