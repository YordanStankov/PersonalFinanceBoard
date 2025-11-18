using FinanceDashboard.Domain.Interfaces;
using FinanceDashboard.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Infrastructure.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckForExistenceAsync(string userId, DateTime transactionDate)
        {
            bool check = await _context.Transactions
               .AnyAsync(t => t.UserId == userId && t.Date == transactionDate);
            return check;
        }

        public async Task<Guid> CreateAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction.Guid;
        }

        public async Task<Transaction> GetAsync(Guid guid)
        {
            return await _context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Guid == guid);
        }
        public async Task<List<int>> GetDayOfOldestAMonthBackAsync(string userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId && (t.Date.Month >= (DateTime.Now.Month - 1) || t.Date.Month == DateTime.Now.Month))
                .OrderByDescending(t => t.Date)
                .Select(t => t.Date.Day)
                .Take(1)
                .ToListAsync();
        }

        public async Task<List<decimal>> GetAllAmountsAsync(string userId, int oldest)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId && t.Date.Month >= oldest)
                .Select(t => t.Amount)
                .ToListAsync();
        }

        public async Task<List<int>> GetMonthOfOldestAsync(string userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date.Day)
                .Select(t => t.Date.Month)
                .Take(1)
                .ToListAsync();
        }

        public async Task<List<decimal>> GetAmountsAMonthBackAsync(string userId)
        {
            return await _context.Transactions
                .AsNoTracking()
                .Where(t => t.UserId == userId && t.Date.Day >= (DateTime.Now.Day - 30))
                .Select(t => t.Amount)
                .ToListAsync();
        }
    }
}
