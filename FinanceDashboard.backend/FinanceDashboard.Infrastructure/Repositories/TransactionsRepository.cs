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
    }
}
