
using FinanceDashboard.Application.DTOs.Transaction;
using FinanceDashboard.Application.DTOs.Transaction.Result;

namespace FinanceDashboard.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<CreateTransactionResultDTO> CreateAsync(CreateTransactionDTO createTransactionDTO);
        Task<TransactionDTO?> GetByGuidAsync(Guid transactionGuid);
    }
}
