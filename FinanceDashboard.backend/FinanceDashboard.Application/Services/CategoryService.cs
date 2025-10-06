
using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.DTOs.Transaction;
using FinanceDashboard.Application.Interfaces;
using FinanceDashboard.Domain.Interfaces;

namespace FinanceDashboard.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> CreateCategoryFSAsync(CreateCategoryDTO dto)
        {
            var category = await _categoryRepository.CreateCategoryAsync(dto.UserId, dto.Name);
            if (category == null)
                return false;
            else
                return true;
        }

        public async Task<List<CategoryListDTO>> GetAllCategoriesOfOneUserAsync(string userId)
        {
            var categories = await _categoryRepository
                .GetAllCategoriesOfUserAsync(userId);
            if (categories != null)
            {
                return categories.Select(c => new CategoryListDTO
                {
                    Name = c.Name,
                    TransactionListDTOs = c.Transactions.Select(t => new TransactionListDTO
                    {
                        TimeOfTransaction = t.Date,
                        Description = t.Description,
                        Amount = t.Amount
                    }).ToList()
                }).ToList();
            }
            else 
                return new List<CategoryListDTO>();
        }
    }
}
