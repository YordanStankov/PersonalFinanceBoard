

namespace FinanceDashboard.Application.DTOs.Transaction
{
    public class CreateTransactionDTO
    {
        public decimal Amount { get; set; } = 0;
        public DateTime Date { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; } = null;
        public string? UserId { get; set; } = null;

    }
}
