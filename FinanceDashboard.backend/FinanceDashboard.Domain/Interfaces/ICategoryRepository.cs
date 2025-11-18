
using FinanceDashboard.Domain.Models;

namespace FinanceDashboard.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Guid> CreateAsync(Category category);
        Task<List<Category>> GetAllOfUserAsync(string userId);
        Task<Guid> GetGuidAsync(string userId, string categoryName);
        Task<Category> GetAsync(string userId, string name);
    }
}
