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
        public async Task<Transaction> CreateTransactionAsync(string userId, decimal amount, Guid categoryGuid, string description, DateTime date)
        {
            var check = await _context.Transactions.AnyAsync(t => t.UserId == userId && t.Amount == amount && t.CategoryGuid == categoryGuid);
            if (check == true)
            {
                throw new InvalidOperationException("Transaction already exists.");
            }
            else if (check == false)
            {
                var trans = new Transaction
                {
                    Amount = amount,
                    Date = date,
                    CategoryGuid = categoryGuid,
                    Description = description,
                    UserId = userId
                };
                _context.Transactions.Add(trans);
                await _context.SaveChangesAsync();
                return trans;
            }
            return new Transaction();
        }

        
    }
}
