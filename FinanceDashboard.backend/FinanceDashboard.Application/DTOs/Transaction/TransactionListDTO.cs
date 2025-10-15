
namespace FinanceDashboard.Application.DTOs.Transaction
{
    public class TransactionListDTO
    {
        public Guid Guid { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public DateTime TimeOfTransaction { get; set; }
        public decimal Amount { get; set; } = 0;
    }
}
