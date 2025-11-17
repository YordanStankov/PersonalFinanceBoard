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

        public async Task<bool> CheckForTransactionAsync(string userId, DateTime transactionDate)
        {
            bool check = await _context.Transactions
               .AnyAsync(t => t.UserId == userId && t.Date == transactionDate);
            return check;
        }

        public async Task<Guid> CreateTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction.Guid;
        }

        public async Task<Transaction> GetTransactionAsync(Guid guid)
        {
            return await _context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Guid == guid);
        }
        public async Task<List<decimal>> GetAllTransactionAmountsWeekBeforeAsync(string userId)
        {
            var result = await _context.Transactions
                .Where(t => t.UserId == userId && t.Date.Day >= (DateTime.Now.Day - 7))
                .Select(t => t.Amount)
                .ToListAsync();

            return result;
        }

        public async Task<List<decimal>> GetAllTransactionAmountsAsync(string userId, int oldest)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId && t.Date.Month >= oldest)
                .Select(t => t.Amount)
                .ToListAsync();
        }

        public async Task<List<int>> GetMonthOfOldestTransactionAsync(string userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date.Day)
                .Select(t => t.Date.Month)
                .Take(1)
                .ToListAsync();
        }
    }
}
