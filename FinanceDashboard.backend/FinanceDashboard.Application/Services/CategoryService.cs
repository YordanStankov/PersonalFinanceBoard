
using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.DTOs.Category.Result;
using FinanceDashboard.Application.DTOs.Transaction;
using FinanceDashboard.Application.Interfaces;
using FinanceDashboard.Domain.Interfaces;
using FinanceDashboard.Domain.Models;

namespace FinanceDashboard.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<CreateCategoryResult> CreateAsync(CreateCategoryDTO dto)
        {
            CreateCategoryResult result = new CreateCategoryResult();
            var category = await _categoryRepository
                .GetAsync(dto.UserId, dto.Name);

            var check = await _userRepository
                .CheckExistenceAsync(dto.UserId);

            if (category == null && check == true)
            {
                Category newCategory = new Category
                {
                    Guid = Guid.NewGuid(),
                    Name = dto.Name,
                    UserId = dto.UserId
                };
                result.Guid = await _categoryRepository.CreateAsync(newCategory);
                result.IsSuccess = true;
            }
            else if (category != null)
            {
                result.IsSuccess = false;
                result.Error = "Category already exists.";
            }
            else if (check == false)
            {
                result.IsSuccess = false;
                result.Error = "User does not exist.";
            }
                return result;
        }

        public async Task<List<CategoryListDTO>> GetAllOfOneUserAsync(string userId)
        {
            var categories = await _categoryRepository
                .GetAllOfUserAsync(userId);
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
