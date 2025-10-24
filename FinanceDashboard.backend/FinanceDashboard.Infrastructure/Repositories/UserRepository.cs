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

        public Task<User> GetUserAsync(string userId)
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
            if (await _context.Users.AnyAsync(u => u.UserName == userName))
            {
                throw new Exception($"Username {userName} is already taken.");
            }
            else if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                throw new Exception($"Email {email} is already registered.");
            }
            else if (password.Length < 6)
            {
                throw new Exception("Password must be at least 6 characters long.");
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
                    return await _context.Users
                        .FirstOrDefaultAsync(u => u.Id == user.Id)
                        ?? throw new KeyNotFoundException($"User with userName {userName} not found.");
                }
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
