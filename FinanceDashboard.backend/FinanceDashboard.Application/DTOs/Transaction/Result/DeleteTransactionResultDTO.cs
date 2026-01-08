
namespace FinanceDashboard.Application.DTOs.Transaction.Result
{
    public class TransactionDeletionResultDTO
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; } = null;
    }
}
