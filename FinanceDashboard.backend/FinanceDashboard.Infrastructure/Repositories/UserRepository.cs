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

        public Task<User> GetUser(string userId)
        {
           return _context.Users
                .Include(u => u.Categories)
                .Include(u => u.Transactions)
                .FirstOrDefaultAsync(u => u.Id == userId) 
                ?? throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        public async Task<User> LoginUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }
            return null;
        }

        public async Task<User> RegisterUser(string userName, string email, string password)
        {
            var user = new User
            {
                UserName = userName,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == userName)
                    ?? throw new KeyNotFoundException($"User with username {userName} not found.");
            }
            throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
        }
    }
}
