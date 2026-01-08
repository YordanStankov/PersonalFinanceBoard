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
            return await _context.Transactions
               .AnyAsync(t => t.UserId == userId && t.Date == transactionDate);

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
        

        public async Task<List<decimal>> GetAllAmountsAsync(string userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .Select(t => t.Amount)
                .ToListAsync();
        }

        public async Task<List<DateTime>> GetDateOfOldestAsync(string userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date.Day)
                .Select(t => t.Date)
                .Take(1)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(Transaction trans)
        {
            _context.Transactions.Remove(trans);
            await _context.SaveChangesAsync();
            return await _context.Transactions.AnyAsync(t => t.Guid == trans.Guid);
        }
    }
}
