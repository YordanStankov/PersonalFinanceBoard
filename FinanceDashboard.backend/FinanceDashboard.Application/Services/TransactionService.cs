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
        public async Task<CreateTransactionResultDTO> CreateTransactionAsync(CreateTransactionDTO dto)
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
                    .CheckForTransactionAsync(dto.UserId, dto.Date);

                Guid categoryGuid = await _categoryRepository
                    .GetCategoryGuidAsync(dto.UserId, dto.CategoryName);

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
                    resultDTO.TransactionGuid = await _transactionsRepository.CreateTransactionAsync(transaction);
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

        public async Task<TransactionDTO?> GetTransactionByGuidAsync(Guid transactionGuid)
        {
            var transaction = await _transactionsRepository.GetTransactionAsync(transactionGuid);
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
    }
}
