
using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.DTOs.Category.Result;
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

        public async Task<CreateCategoryResult> CreateCategoryAsync(CreateCategoryDTO dto)
        {
            var category = await _categoryRepository.CreateCategoryAsync(dto.UserId, dto.Name);
            CreateCategoryResult result = new CreateCategoryResult();
            if (category == null)
            {
                result.IsSuccess = false;
                result.Guid = Guid.Empty;
            }
            else
            {
                result.IsSuccess = true;
                result.Guid = category.Guid;
            }
               
            return result;
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
