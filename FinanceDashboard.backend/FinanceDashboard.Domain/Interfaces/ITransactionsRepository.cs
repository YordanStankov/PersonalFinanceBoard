using FinanceDashboard.Domain.Models;

namespace FinanceDashboard.Domain.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<bool> CheckForExistenceAsync(string userId, DateTime transactionDate);
        Task<Guid> CreateAsync(Transaction transaction);
        Task<Transaction> GetAsync(Guid guid);
        Task<List<int>> GetDayOfOldestAMonthBackAsync(string userId);
        Task<List<decimal>> GetAmountsAMonthBackAsync(string userId);
        Task<List<decimal>> GetAllAmountsAsync(string userId, int oldest);
        Task<List<int>> GetMonthOfOldestAsync(string userId);
    }
}
