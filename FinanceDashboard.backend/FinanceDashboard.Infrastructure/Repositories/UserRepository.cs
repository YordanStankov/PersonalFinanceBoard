using FinanceDashboard.Domain.Interfaces;
using FinanceDashboard.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public async Task<bool> CheckUserExistenceAsync(string userId)
        {
            return await _context.Users
                .AnyAsync(u => u.Id == userId);
        }

        public async Task<User> GetUserAsync(string userId)
        {
            return await _context.Users
                 .Include(u => u.Categories)
                 .Include(u => u.Transactions)
                 .FirstOrDefaultAsync(u => u.Id == userId) ?? new User();
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

        public async Task<string> RegisterUser(string userName, string email, string password)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == userName))
            {
                return "Error: Username is already taken.";
            }
            else if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                return $"Error: Email {email} is already registered.";
            }
            else
            {
                var user = new User
                {
                    UserName = userName,
                    Email = email
                };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _context.SaveChangesAsync();
                    return user.Id;
                }
                return $"Error: User registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}";
            }
        }
    }
}
