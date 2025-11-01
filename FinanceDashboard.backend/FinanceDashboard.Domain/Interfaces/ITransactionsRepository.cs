using FinanceDashboard.Domain.Models;

namespace FinanceDashboard.Domain.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<bool> CheckForTransactionAsync(string userId, DateTime transactionDate);
        Task<Guid> CreateTransactionAsync(Transaction transaction);
        Task<Transaction> GetTransactionAsync(Guid guid);
    }
}
