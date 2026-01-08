using FinanceDashboard.Application.DTOs.Transaction;
using FinanceDashboard.Application.DTOs.Transaction.Result;
using FinanceDashboard.Application.Interfaces;
using FinanceDashboard.Domain.Interfaces;
using FinanceDashboard.Domain.Models;

namespace FinanceDashboard.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionsRepository _transactionsRepository;
        public readonly ICategoryRepository _categoryRepository;
        public TransactionService(ITransactionsRepository transactionsRepository, ICategoryRepository categoryRepository)
        {
            _transactionsRepository = transactionsRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<CreateTransactionResultDTO> CreateAsync(CreateTransactionDTO dto)
            {
            CreateTransactionResultDTO resultDTO = new CreateTransactionResultDTO();
            if(dto.UserId == null)
            {
                resultDTO.Success = false;
                resultDTO.ErrorMessage = "UserId is null when creating transaction";
            }
            else
            {
                bool check = await _transactionsRepository
                    .CheckForExistenceAsync(dto.UserId, dto.Date);

                Guid categoryGuid = await _categoryRepository
                    .GetGuidAsync(dto.UserId, dto.CategoryName);

                if (check == false && categoryGuid != Guid.Empty)
                {
                    Transaction transaction = new Transaction
                    {
                        Guid = Guid.NewGuid(),
                        Amount = dto.Amount,
                        Date = dto.Date,
                        CategoryGuid = categoryGuid,
                        Description = dto.Description,
                        UserId = dto.UserId
                    };
                    resultDTO.TransactionGuid = await _transactionsRepository.CreateAsync(transaction);
                    resultDTO.Success = true;
                }
                else
                {
                    resultDTO.Success = false;
                    if (check == true)
                        resultDTO.ErrorMessage = "Transaction already exists at this exact time";

                    else if (categoryGuid == Guid.Empty)
                        resultDTO.ErrorMessage = "Category does not exist";
                }
            }
            return resultDTO;
        }

        public async Task<TransactionDTO?> GetByGuidAsync(Guid transactionGuid)
        {
            var transaction = await _transactionsRepository.GetAsync(transactionGuid);
            TransactionDTO? transactionDTO =  new TransactionDTO
            {
                Guid = transaction.Guid,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Description = transaction.Description,
                CategoryGuid = transaction.CategoryGuid,
                UserId = transaction.UserId
            };
            return transactionDTO;
        }

        public async Task<TransactionDeletionResultDTO> DeleteAsync(Guid guid)
        {
            Transaction trans = await _transactionsRepository.GetAsync(guid);
            TransactionDeletionResultDTO result = new TransactionDeletionResultDTO();
            if (trans is null) { 
                result.Success = false;
                result.ErrorMessage = "Transaction was not found";
                }
            else
            {
                bool deleted = await _transactionsRepository.DeleteAsync(trans);
                if (!deleted)
                    result.Success = true;
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "Deletion failed!";
                }
            }
            return result;
        }
    }
}
