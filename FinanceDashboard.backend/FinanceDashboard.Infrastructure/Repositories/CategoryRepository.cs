
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

        public async Task<Guid> CreateAsync(Category category)
        {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return category.Guid;
        }

        public async Task<List<Category>> GetAllOfUserAsync(string userId)
        {
            return await _context.Categories
                .Include(c => c.Transactions)
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Category> GetAsync(string userId, string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name && c.UserId == userId);
        }

        public async Task<Guid> GetGuidAsync(string userId, string categoryName)
        {
            return await _context.Categories
               .Where(c => c.UserId == userId && c.Name == categoryName)
               .Select(c => c.Guid)
               .FirstAsync();
        }
    }
}
