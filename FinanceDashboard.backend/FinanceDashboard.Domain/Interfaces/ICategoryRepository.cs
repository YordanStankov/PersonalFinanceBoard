
using FinanceDashboard.Domain.Models;

namespace FinanceDashboard.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(string userId, string name);
        Task<List<Category>> GetAllCategoriesOfUserAsync(string userId);
    }
}
