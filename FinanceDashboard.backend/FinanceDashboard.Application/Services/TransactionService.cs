using FinanceDashboard.Application.DTOs.Transaction;
using FinanceDashboard.Application.DTOs.Transaction.Result;
using FinanceDashboard.Application.Interfaces;
using FinanceDashboard.Domain.Interfaces;

namespace FinanceDashboard.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionsRepository _transactionsRepository;
        public TransactionService(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }
        public async Task<CreateTransactionResultDTO> CreateTransactionAsync(CreateTransactionDTO dto)
        {
            try
            {
                var transaction = await _transactionsRepository.CreateTransactionAsync(
                dto.UserId!,
                dto.Amount,
                dto.CategoryGuid,
                dto.Description!,
                dto.Date);
                return new CreateTransactionResultDTO
                {
                    TransactionGuid = transaction.Guid,
                    Success = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new CreateTransactionResultDTO
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
