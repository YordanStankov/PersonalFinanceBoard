namespace FinanceDashboard.Application.DTOs.Transaction.Result
{
    public class CreateTransactionResultDTO
    {
        public Guid TransactionGuid { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; } = null;   
    }
}
