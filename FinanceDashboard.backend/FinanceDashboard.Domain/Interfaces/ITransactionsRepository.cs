using FinanceDashboard.Domain.Models;

namespace FinanceDashboard.Domain.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<Transaction> CreateTransactionAsync(string userId, decimal amount, Guid categoryGuid, string description, DateTime date);
    }
}
