
using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.DTOs.Category.Result;

namespace FinanceDashboard.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CreateCategoryResult> CreateAsync(CreateCategoryDTO dto);
        Task<List<CategoryListDTO>> GetAllOfOneUserAsync(string userId);
    }
}
