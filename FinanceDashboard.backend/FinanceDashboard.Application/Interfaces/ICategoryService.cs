
using FinanceDashboard.Application.DTOs.Category;

namespace FinanceDashboard.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryFSAsync(CreateCategoryDTO dto);
        Task<List<CategoryListDTO>> GetAllCategoriesOfOneUserAsync(string userId);
    }
}
