using FinanceDashboard.Domain.Interfaces;
using FinanceDashboard.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<User> GetUser(string userId)
        {
           return _context.Users
                .Include(u => u.Categories)
                .Include(u => u.Transactions)
                .FirstOrDefaultAsync(u => u.Id == userId) 
                ?? throw new KeyNotFoundException($"User with ID {userId} not found.");
        }
    }
}
