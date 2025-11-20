using FinanceDashboard.Domain.Interfaces;
using FinanceDashboard.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CheckExistenceAsync(string userId)
        {
            return await _context.Users
                .AnyAsync(u => u.Id == userId);
        }

        public async Task<bool> CheckForExistingEmailAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<bool> CheckForExistingUserNameAsync(string userName)
        {
            return await _context.Users
                .AnyAsync(u => u.UserName == userName);
        }

        public async Task<User> GetAsync(string userId)
        {
            return await _context.Users
                 .Include(u => u.Categories)
                 .Include(u => u.Transactions)
                 .FirstOrDefaultAsync(u => u.Id == userId) ?? new User();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
           return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? new User();
        }

        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
