
using FinanceDashboard.Domain.Interfaces;
using FinanceDashboard.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(string userId, string name)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name && c.UserId == userId);
            if (category == null)
            {
                Category cat = new Category
                {
                    Name = name,
                    UserId = userId,
                };
                await _context.Categories.AddAsync(cat);
                await _context.SaveChangesAsync();
                return cat;
            }
            else
                throw new Exception("Category already present in DB for User");
        }

        public async Task<List<Category>> GetAllCategoriesOfUserAsync(string userId)
        {
            return await _context.Categories
                .Include(c => c.Transactions)
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}
