
using FinanceDashboard.Domain.Models;

namespace FinanceDashboard.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Guid> CreateCategoryAsync(Category category);
        Task<List<Category>> GetAllCategoriesOfUserAsync(string userId);
        Task<Guid> GetCategoryGuidAsync(string userId, string categoryName);
        Task<Category> GetCategoryAsync(string userId, string name);
    }
}
