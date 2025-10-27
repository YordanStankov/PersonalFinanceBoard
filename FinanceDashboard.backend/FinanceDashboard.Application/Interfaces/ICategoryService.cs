
using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.DTOs.Category.Result;

namespace FinanceDashboard.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CreateCategoryResult> CreateCategoryAsync(CreateCategoryDTO dto);
        Task<List<CategoryListDTO>> GetAllCategoriesOfOneUserAsync(string userId);
    }
}
