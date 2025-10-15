using FinanceDashboard.Domain.Models;
using System.Security.Claims;
namespace FinanceDashboard.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string userId);
        Task<User> RegisterUser(string userName, string email, string password);
        Task<User> LoginUser(string email, string password);
        Task<User> GetUserByClaimAsync(ClaimsPrincipal userClaim);
    }
}
