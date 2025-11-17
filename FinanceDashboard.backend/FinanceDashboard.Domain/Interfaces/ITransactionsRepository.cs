using FinanceDashboard.Domain.Models;

namespace FinanceDashboard.Domain.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<bool> CheckForTransactionAsync(string userId, DateTime transactionDate);
        Task<Guid> CreateTransactionAsync(Transaction transaction);
        Task<Transaction> GetTransactionAsync(Guid guid);
        Task<List<decimal>> GetAllTransactionAmountsWeekBeforeAsync(string userId); 
        Task<List<decimal>> GetAllTransactionAmountsAsync(string userId, int oldest);
        Task<List<int>> GetMonthOfOldestTransactionAsync(string userId);
    }
}
