
using FinanceDashboard.Domain.Models;

namespace FinanceDashboard.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> RetrieveAllUsers();
    }
}
