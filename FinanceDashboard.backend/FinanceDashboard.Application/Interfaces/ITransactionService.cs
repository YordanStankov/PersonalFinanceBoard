
using FinanceDashboard.Application.DTOs.Transaction;
using FinanceDashboard.Application.DTOs.Transaction.Result;

namespace FinanceDashboard.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<CreateTransactionResultDTO> CreateTransactionAsync(CreateTransactionDTO createTransactionDTO);
        Task<TransactionDTO?> GetTransactionByGuidAsync(Guid transactionGuid);
    }
}
