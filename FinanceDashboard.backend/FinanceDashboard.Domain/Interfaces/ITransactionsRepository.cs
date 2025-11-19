using FinanceDashboard.Domain.Models;

namespace FinanceDashboard.Domain.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<bool> CheckForExistenceAsync(string userId, DateTime transactionDate);
        Task<Guid> CreateAsync(Transaction transaction);
        Task<Transaction> GetAsync(Guid guid);
        Task<List<decimal>> GetAllAmountsAsync(string userId);
        Task<List<DateTime>> GetDateOfOldestAsync(string userId);
    }
}
